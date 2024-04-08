using System.Runtime.CompilerServices;
using AutoMapper;
using Essentials.Results;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Data.Models.Auth;
using PMS.Data.Models.PharmacyEntities;
using PMS.Services.Contracts.Pharmacy;
using PMS.Shared.Models.Pharmacies;
using PMS.Shared.Models.User;

[assembly: InternalsVisibleTo("PMS.Tests")]

namespace PMS.Services.Implementations.Pharmacy;

/// <summary>
/// A class responsible for pharmacy-related operations.
/// </summary>
internal class PharmacyService : IPharmacyService
{
    private readonly ApplicationDbContext context;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="PharmacyService"/> class.
    /// </summary>
    /// <param name="context">Application database context.</param>
    /// <param name="mapper">Auto mapper.</param>
    /// <param name="userManager">User manager.</param>
    /// <param name="roleManager">Role manager.</param>
    public PharmacyService(ApplicationDbContext context, IMapper mapper, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        this.context = context;
        this.mapper = mapper;
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    /// <inheritdoc />
    public async Task AssignEmployeeAsync(string pharmacyId, string email)
    {
        var pharmacist = await this.userManager.FindByEmailAsync(email);

        if (pharmacist is null)
        {
            throw new ArgumentException("Невалиден имейл");
        }

        if (!await this.roleManager.RoleExistsAsync(UserRoles.Pharmacist))
        {
            await this.roleManager.CreateAsync(new IdentityRole(UserRoles.Pharmacist));
        }

        var result = await this.userManager.AddToRoleAsync(pharmacist, UserRoles.Pharmacist);

        if (!result.Succeeded)
        {
            throw new Exception("Излезе грешка при добавянето на потребителя към ролята фармацевт");
        }

        var pharmacy = await this.context.Pharmacies
            .Include(p => p.Pharmacists)
            .FirstOrDefaultAsync(p => p.Id == pharmacyId);

        pharmacy.Pharmacists.Add(pharmacist);

        await this.context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<bool> CheckIfUserHasAnyPharmacies(string userId)
    {
        var pharmacies = await this.context.Pharmacies
            .Include(p => p.Founder)
            .Where(p => p.FounderId == userId)
            .ToListAsync();

        return pharmacies.Count > 0;
    }

    /// <inheritdoc />
    public async Task<MutationResult> CreatePharmacyAsync(PharmacyIM pharmacyIM, string founderId)
    {
        var result = await this.context.Pharmacies.Where(p => p.Name == pharmacyIM.Name).ToListAsync();

        if (result.Count > 0)
        {
            throw new Exception("Името на аптеката вече съществува");
        }

        var pharmacy = new Data.Models.PharmacyEntities.Pharmacy
        {
            Id = Guid.NewGuid().ToString(),
            Name = pharmacyIM.Name,
            Description = pharmacyIM.Description,
            DepotId = pharmacyIM.DepotId,
            FounderId = founderId,
            Address = pharmacyIM.Address,
        };

        await this.context.Pharmacies.AddAsync(pharmacy);
        await this.context.SaveChangesAsync();

        return MutationResult.ResultFrom(pharmacy.Id, "Аптеката бе създадена");
    }

    /// <inheritdoc />
    public async Task DeletePharmacyAsync(string id)
    {
        var pharmacy = await this.context.Pharmacies
            .Include(p => p.Pharmacists)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pharmacy is null)
        {
            throw new ArgumentException("Невалидно \"id\"");
        }

        foreach (var pharmacist in pharmacy.Pharmacists)
        {
            var result = await this.userManager.DeleteAsync(pharmacist);

            if (!result.Succeeded)
            {
                throw new Exception("Грешка при премахването на фармацевт от аптеката");
            }
        }

        this.context.Pharmacies.Remove(pharmacy);

        await this.context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<ICollection<PharmacyVM>> GetPharmaciesAsync(string userId)
    {
        var pharmacyQuery = this.context.Pharmacies
            .Where(p => p.FounderId == userId || p.Pharmacists.Select(phst => phst.Id).Contains(userId))
            .Include(p => p.Depot)
            .Include(p => p.Founder)
            .Include(p => p.Depot.Medicines)
                .ThenInclude(m => m.BasicMedicine)
            .Include(p => p.Depot.Manager)
            .Include(p => p.Medicines)
                .ThenInclude(m => m.BasicMedicine)
            .AsNoTracking()
            .AsQueryable();

        return this.mapper.Map<List<PharmacyVM>>(pharmacyQuery);
    }

    /// <inheritdoc />
    public async Task<ICollection<UserVM>> GetPharmacistsAsync(string pharmacyId)
    {
        var pharmacists = (await this.context.Pharmacies
            .Include(p => p.Pharmacists)
            .FirstOrDefaultAsync(p => p.Id == pharmacyId)).Pharmacists;

        return this.mapper.Map<List<UserVM>>(pharmacists);
    }

    /// <inheritdoc />
    public async Task<PharmacyVM> GetPharmacyByIdAsync(string id)
    {
        var pharmacy = await this.context.Pharmacies
            .Include(p => p.Depot)
            .Include(p => p.Founder)
            .Include(p => p.Medicines)
                .ThenInclude(m => m.BasicMedicine)
            .Include(p => p.Pharmacists)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (pharmacy is null)
        {
            throw new ArgumentException("Невалидно \"id\"");
        }

        return this.mapper.Map<PharmacyVM>(pharmacy);
    }

    /// <inheritdoc />
    public async Task<ICollection<PharmacyVM>> GetPharmaiesByDepotId(string depotId)
    {
        var pharmacyQuery = this.context.Pharmacies
            .Where(p => p.DepotId == depotId)
            .Include(p => p.Depot)
            .Include(p => p.Depot.Medicines)
                .ThenInclude(m => m.BasicMedicine)
            .Include(p => p.Depot.Manager)
            .Include(p => p.Founder)
            .Include(p => p.Medicines)
                .ThenInclude(m => m.BasicMedicine)
            .Include(p => p.Pharmacists)
            .AsNoTracking()
            .AsQueryable();

        return this.mapper.Map<List<PharmacyVM>>(pharmacyQuery);
    }

    /// <inheritdoc />
    public async Task RemoveEmployeeAsync(string pharmacyId, string employeeId)
    {
        var pharmacy = await this.context.Pharmacies
            .Include(p => p.Pharmacists)
            .FirstOrDefaultAsync(p => p.Id == pharmacyId);

        if (pharmacy is null)
        {
            throw new ArgumentException("Невалидно \"id\" на аптеката");
        }

        var pharmacist = pharmacy.Pharmacists.FirstOrDefault(ph => ph.Id == employeeId);

        if (pharmacist is null)
        {
            throw new ArgumentException("Invalid ID of employee");
        }

        var result = await this.userManager.DeleteAsync(pharmacist);

        if (!result.Succeeded)
        {
            throw new Exception("Failed to remove the pharmacist");
        }
    }

    /// <inheritdoc />
    public async Task RequestDepotAssignAsync(string pharmacyId, string depotId)
    {
        var pharmacy = await this.context.Pharmacies.FindAsync(pharmacyId);

        var notification = await this.context.Notifications
            .Include(n => n.Pharmacy)
            .Include(n => n.Depot)
            .AsNoTracking()
            .FirstOrDefaultAsync(n => n.IsAssignRequest && n.PharmacyId == pharmacyId);

        if (notification is not null)
        {
            throw new Exception("Нова заявка не може да бъде направена преди да е приета старата");
        }

        var newNotification = new Notification
        {
            Id = Guid.NewGuid().ToString(),
            Text = $"Аптека с \"id\": {pharmacyId} подаде заявка за работа с вас",
            SentOn = DateOnly.FromDateTime(DateTime.UtcNow),
            IsAssignRequest = true,
            PharmacyId = pharmacyId,
            DepotId = depotId,
            IsWarning = false,
        };

        await this.context.Notifications.AddAsync(newNotification);

        await this.context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task UpdatePharmacyAsync(PharmacyUM pharmacyUM)
    {
        var pharmacy = await this.context.Pharmacies.FindAsync(pharmacyUM.Id);
        if (pharmacy is null)
        {
            throw new ArgumentException("Невалидно \"id\"");
        }

        pharmacy.Name = pharmacyUM.Name ?? pharmacy.Name;
        pharmacy.Description = pharmacyUM.Description ?? pharmacy.Description;
        pharmacy.Address = pharmacyUM.Address ?? pharmacy.Address;
        pharmacy.FounderId = pharmacyUM.FounderId ?? pharmacy.FounderId;
        pharmacy.DepotId = pharmacyUM.DepotId ?? pharmacy.DepotId;

        await this.context.SaveChangesAsync();
    }
}