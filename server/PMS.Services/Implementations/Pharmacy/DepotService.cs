using System.Runtime.CompilerServices;
using AutoMapper;
using Essentials.Results;
using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Data.Models.PharmacyEntities;
using PMS.Services.Contracts.Pharmacy;
using PMS.Shared.Models.Depots;

[assembly: InternalsVisibleTo("PMS.Tests")]

namespace PMS.Services.Implementations.Pharmacy;

/// <summary>
/// A class responsible for depot-related operations.
/// </summary>
internal class DepotService : IDepotService
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="DepotService"/> class.
    /// </summary>
    /// <param name="context">Application database context.</param>
    /// <param name="mapper">Auto mapper.</param>
    public DepotService(ApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task AssignDepotToPharmacyAsync(string depotId, string pharmacyId)
    {
        var depot = await this.context.Depots.FindAsync(depotId);
        if (depot is null)
        {
            throw new ArgumentException("Невалидно \"id\" на склада");
        }

        var pharmacy = await this.context.Pharmacies.FindAsync(pharmacyId);
        if (pharmacy is null)
        {
            throw new ArgumentException("Невалидно \"id\" на аптеката");
        }

        var notification = await this.context.Notifications
            .Include(n => n.Pharmacy)
            .Include(n => n.Depot)
            .FirstOrDefaultAsync(n => n.IsAssignRequest && n.PharmacyId == pharmacyId && n.DepotId == depotId);

        if (notification is null)
        {
            throw new Exception("Няма заявка за добавяне на този склад към тази аптека");
        }

        this.context.Notifications.Remove(notification);

        pharmacy.Depot = depot;
        await this.context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<MutationResult> CreateDepotAsync(DepotIM depotIM, string managerId)
    {
        var result = await this.context.Depots.Where(d => d.Name == depotIM.Name).ToListAsync();

        if (result.Count > 0)
        {
            throw new Exception("Името на склада вече съществува");
        }

        var depot = new Depot
        {
            Id = Guid.NewGuid().ToString(),
            Name = depotIM.Name,
            Address = depotIM.Address,
            ManagerId = managerId,
        };
        depot.Id = Guid.NewGuid().ToString();

        await this.context.Depots.AddAsync(depot);
        await this.context.SaveChangesAsync();

        return MutationResult.ResultFrom(depot.Id, "Складът беше създаден успешно");
    }

    /// <inheritdoc />
    public async Task DeleteDepotAsync(string id)
    {
        var depot = await this.context.Depots.FindAsync(id);
        if (depot is null)
        {
            throw new ArgumentException("Невалидно \"id\"");
        }

        this.context.Depots.Remove(depot);
        await this.context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<ICollection<DepotVM>> GetAllDepotsAsync()
    {
        var depots = await this.context.Depots
            .Include(d => d.Manager)
            .Include(d => d.Medicines)
                .ThenInclude(m => m.BasicMedicine)
            .AsNoTracking()
            .ToListAsync();

        return this.mapper.Map<List<DepotVM>>(depots);
    }

    /// <inheritdoc />
    public async Task<DepotVM> GetDepotByIdAsync(string id)
    {
        var depot = await this.context.Depots
            .Include(d => d.Medicines)
                .ThenInclude(d => d.BasicMedicine)
            .Include(d => d.Manager)
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == id);

        if (depot is null)
        {
            throw new ArgumentException("Невалидно \"id\"");
        }

        return this.mapper.Map<DepotVM>(depot);
    }

    public async Task<ICollection<DepotVM>> GetDepotsByUserIdAsync(string userId)
    {
        var depots = await this.context.Depots
            .Include(d => d.Manager)
            .Include(d => d.Medicines)
                .ThenInclude(m => m.BasicMedicine)
            .Where(d => d.ManagerId == userId)
            .AsNoTracking()
            .ToListAsync();

        return this.mapper.Map<List<DepotVM>>(depots);
    }

    /// <inheritdoc />
    public async Task UpdateDepotAsync(DepotUM depotUM)
    {
        var depot = await this.context.Depots
            .Include(d => d.Manager)
            .FirstOrDefaultAsync(d => d.Id == depotUM.Id);

        if (depot is null)
        {
            throw new ArgumentException("Невалидно \"id\"");
        }

        depot.Name = depotUM.Name ?? depot.Name;
        depot.ManagerId = depotUM.ManagerId ?? depot.ManagerId;
        depot.Address = depotUM.Address ?? depot.Address;

        await this.context.SaveChangesAsync();
    }
}