using System.Runtime.CompilerServices;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Essentials.Results;
using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Data.Models.PharmacyEntities;
using PMS.Services.Contracts;
using PMS.Services.Contracts.Pharmacy;
using PMS.Services.Enums;
using PMS.Shared.Models.Medicines;

[assembly: InternalsVisibleTo("PMS.Tests")]

namespace PMS.Services.Implementations.Pharmacy;

/// <summary>
/// A class responsible for medicine-related operations.
/// </summary>
internal class MedicineService : IMedicineService
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;
    private readonly IFileService fileService;

    /// <summary>
    /// Initializes a new instance of the <see cref="MedicineService"/> class.
    /// </summary>
    /// <param name="context">Application database context.</param>
    /// <param name="mapper">Auto mapper.</param>
    /// <param name="fileService">The file service.</param>
    public MedicineService(ApplicationDbContext context, IMapper mapper, IFileService fileService)
    {
        this.context = context;
        this.mapper = mapper;
        this.fileService = fileService;
    }

    /// <inheritdoc />
    public async Task<MutationResult> CreateMedicineAsync(MedicineIM medicineIM, BuildingType buildingType, string buildingId)
    {
        if ((await this.CheckIfMedicineExists(medicineIM.BasicMedicineId, buildingId, buildingType)) != null)
        {
            throw new Exception("Не може да се добави същото лекарство повторно");
        }

        Data.Models.PharmacyEntities.Pharmacy? pharmacy = null;

        if (buildingType is BuildingType.Pharmacy)
        {
            pharmacy = await this.context.Pharmacies
                .Include(p => p.Depot)
                .FirstOrDefaultAsync(p => p.Id == buildingId);

            if (pharmacy is null)
            {
                throw new ArgumentException("Невалидно \"id\" на сградата");
            }

            var depotMedicineId = await this.CheckIfMedicineExists(medicineIM.BasicMedicineId, pharmacy.Depot.Id, BuildingType.Depot);

            if (depotMedicineId is null)
            {
                throw new Exception("Складът няма лекарството в наличност в момента");
            }

            var depotMedicine = await this.context.Medicines.FindAsync(depotMedicineId);

            if (medicineIM.Quantity > depotMedicine.Quantity)
            {
                throw new Exception("Складът няма достатъчно бройки от лекарството за да осъществи заявката в момента");
            }

            depotMedicine.Quantity -= medicineIM.Quantity;
            medicineIM.ExpirationDate = (medicineIM.ExpirationDate is null) ? depotMedicine.ExpirationDate.ToString() : medicineIM.ExpirationDate;
        }

        DateOnly? expirationDate = null;

        if (medicineIM.ExpirationDate != null)
        {
            bool isDataParsed = DateOnly.TryParse(medicineIM.ExpirationDate, out DateOnly parsedDate);
            if (!isDataParsed)
            {
                throw new ArgumentException("Невалиден формат за датата");
            }

            expirationDate = parsedDate;
        }
        else
        {
            throw new ArgumentException("Няма дата");
        }

        var medicine = new Medicine
        {
            Id = Guid.NewGuid().ToString(),
            Price = medicineIM.Price,
            Quantity = medicineIM.Quantity,
            BasicMedicineId = medicineIM.BasicMedicineId,
            ExpirationDate = expirationDate ?? DateOnly.FromDateTime(DateTime.Now),
        };

        await this.context.Medicines.AddAsync(medicine);

        if (buildingType is BuildingType.Pharmacy)
        {
            var basicMedicine = await this.context.BasicMedicines.FindAsync(medicine.BasicMedicineId);

            var notification = new Notification
            {
                Id = Guid.NewGuid().ToString(),
                Text = $"Аптека {pharmacy.Name} заяви {medicine.Quantity} бройки от {basicMedicine.Name}.",
                SentOn = DateOnly.FromDateTime(DateTime.UtcNow),
                PharmacyId = null,
                DepotId = pharmacy.DepotId,
                IsWarning = false,
            };

            await this.context.Notifications.AddAsync(notification);
        }

        await this.context.SaveChangesAsync();

        await this.AssignMedicineAsync(medicine.Id, buildingType, buildingId);

        return MutationResult.ResultFrom(medicine.Id, "Лекарството бе създадено успешно");
    }

    /// <inheritdoc />
    public async Task UpdateMedicineAsync(MedicineUM medicineUM)
    {
        var medicine = await this.context.Medicines.FindAsync(medicineUM.Id);
        if (medicine is null)
        {
            throw new ArgumentException("Невалидно \"id\"");
        }

        medicine.Price = medicineUM.Price ?? medicine.Price;
        medicine.Quantity = medicineUM.Count ?? medicine.Quantity;
        if (medicineUM.Image != null)
        {
            medicine.ImageUrl = await this.fileService.UploadImageAsync(medicineUM.Image);
        }

        await this.context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task DeleteMedicineAsync(string id)
    {
        var medicine = await this.context.Medicines.FindAsync(id);
        if (medicine is null)
        {
            throw new ArgumentException("Невалидно \"id\"");
        }

        this.context.Medicines.Remove(medicine);
        await this.context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task<ICollection<MedicineVM>> GetAllMedicinesAsync(BuildingType buildingType, string id)
    {
        if (buildingType is BuildingType.Pharmacy)
        {
            var pharmacy = await this.context.Pharmacies
                .Include(p => p.Medicines)
                    .ThenInclude(p => p.BasicMedicine)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            return pharmacy.Medicines
                .AsQueryable()
                .ProjectTo<MedicineVM>(this.mapper.ConfigurationProvider)
                .ToList();
        }

        var depot = await this.context.Depots
            .Include(p => p.Medicines)
                .ThenInclude(p => p.BasicMedicine)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);

        return depot.Medicines
            .AsQueryable()
            .ProjectTo<MedicineVM>(this.mapper.ConfigurationProvider)
            .ToList();
    }

    /// <inheritdoc />
    public async Task<MedicineVM> GetMedicineByIdAsync(string id)
    {
        var medicine = await this.context.Medicines
            .Include(m => m.BasicMedicine)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (medicine is null)
        {
            throw new ArgumentException("Невалидно \"id\" на лекарството");
        }

        return this.mapper.Map<MedicineVM>(medicine);
    }

    /// <inheritdoc />
    public async Task<ICollection<BasicMedicineVM>> GetBasicMedicinesAsync()
    {
        return await this.context.BasicMedicines
            .ProjectTo<BasicMedicineVM>(this.mapper.ConfigurationProvider)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task RefillQuantityAsync(int quantity, string pharmacyId, string medicineId)
    {
        var requestedMedicine = await this.context.Medicines
            .Include(m => m.BasicMedicine)
            .FirstOrDefaultAsync(rm => rm.Id == medicineId);

        if (requestedMedicine is null)
        {
            throw new ArgumentException("Невалидно \"id\" на лекарството");
        }

        var pharmacy = await this.context.Pharmacies
            .Include(p => p.Depot)
            .FirstOrDefaultAsync(p => p.Id == pharmacyId);

        if (pharmacy is null)
        {
            throw new ArgumentException("Невалидно \"id\" на аптеката");
        }

        var depotMedicineId = await this.CheckIfMedicineExists(requestedMedicine.BasicMedicineId, pharmacy.DepotId!, BuildingType.Depot);

        if (depotMedicineId is null)
        {
            throw new Exception("Складът няма лекарството в наличност в момента");
        }

        var depotMedicine = await this.context.Medicines.FindAsync(depotMedicineId);

        if (quantity > depotMedicine.Quantity)
        {
            throw new Exception("Складът няма достатъчни бройки от лекарството за да бъде осъществена заявката в момента");
        }

        depotMedicine.Quantity -= quantity;
        requestedMedicine.Quantity += quantity;

        var notification = new Notification
        {
            Id = Guid.NewGuid().ToString(),
            Text = $"Аптека {pharmacy.Name} заяви презареждане от {quantity} бройки от {requestedMedicine.BasicMedicine.Name}.",
            SentOn = DateOnly.FromDateTime(DateTime.UtcNow),
            PharmacyId = null,
            DepotId = pharmacy.DepotId,
            IsWarning = false,
        };

        await this.context.Notifications.AddAsync(notification);

        await this.context.SaveChangesAsync();
    }

    private async Task AssignMedicineAsync(string medicineId, BuildingType buildingType, string buildingId)
    {
        var medicine = await this.context.Medicines.FindAsync(medicineId);
        if (medicine is null)
        {
            throw new ArgumentException("Нвалидно \"id\" на лекарството");
        }

        if (buildingType == BuildingType.Pharmacy)
        {
            var pharmacy = await this.context.Pharmacies.FindAsync(buildingId);
            if (pharmacy is null)
            {
                throw new ArgumentException("Невалидно \"id\" на сградата");
            }

            pharmacy.Medicines.Add(medicine);
        }
        else if (buildingType == BuildingType.Depot)
        {
            var depot = await this.context.Depots.FindAsync(buildingId);
            if (depot is null)
            {
                throw new ArgumentException("Невалидно \"id\" на сградата");
            }

            depot.Medicines.Add(medicine);
        }

        await this.context.SaveChangesAsync();
    }

    private async Task<string?> CheckIfMedicineExists(string basicMedicineId, string buildingId, BuildingType buildingType)
    {
        Medicine? medicine;

        if (buildingType == BuildingType.Pharmacy)
        {
            var pharmacy = await this.context.Pharmacies
            .Include(p => p.Medicines)
                .ThenInclude(m => m.BasicMedicine)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == buildingId);

            medicine = pharmacy.Medicines.FirstOrDefault(m => m.BasicMedicineId == basicMedicineId);
        }
        else
        {
            var depot = await this.context.Depots
            .Include(p => p.Medicines)
                .ThenInclude(m => m.BasicMedicine)
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == buildingId);

            medicine = depot.Medicines.FirstOrDefault(m => m.BasicMedicineId == basicMedicineId);
        }

        return (medicine is null) ? null : medicine.Id;
    }
}