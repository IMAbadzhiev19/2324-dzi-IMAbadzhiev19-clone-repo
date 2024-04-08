using Essentials.Results;
using PMS.Services.Enums;
using PMS.Shared.Models.Medicines;

namespace PMS.Services.Contracts.Pharmacy;

/// <summary>
/// Interface for medicine service.
/// </summary>
public interface IMedicineService
{
    /// <summary>
    /// Creates a new medicine asynchronously. If the buildingType is pharmacy, it checks whether the depot which the pharmacy works with has the medicine, if it has it then the quantity is reduced and transfered to the pharmacy's newly added medicine. The expiration date is also transfered from the one the medicine in the depot has.
    /// </summary>
    /// <param name="medicineIM">The medicine input model.</param>
    /// <param name="buildingType">The type of the building.</param>
    /// <param name="buildingId">The id of the building.</param>
    /// <returns>The ID of the newly created medicine.</returns>
    Task<MutationResult> CreateMedicineAsync(MedicineIM medicineIM, BuildingType buildingType, string buildingId);

    /// <summary>
    /// Updates a medicine asynchronously.
    /// </summary>
    /// <param name="medicineUM">The medicine update model.</param>
    /// <returns>A synchronous void task.</returns>
    Task UpdateMedicineAsync(MedicineUM medicineUM);

    /// <summary>
    /// Deletes a medicine asynchronously.
    /// </summary>
    /// <param name="id">The ID of the medicine to delete.</param>
    /// <returns>A synchronous void task.</returns>
    Task DeleteMedicineAsync(string id);

    /// <summary>
    /// Retrieves all medicines asynchronously.
    /// </summary>
    /// <param name="buildingType">The type of the building.</param>
    /// <param name="id">The ID of the building.</param>
    /// <returns>A collection of medicine view models.</returns>
    Task<ICollection<MedicineVM>> GetAllMedicinesAsync(BuildingType buildingType, string id);

    /// <summary>
    /// Retrieves a medicine by ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the medicine.</param>
    /// <returns>The medicine view model.</returns>
    Task<MedicineVM> GetMedicineByIdAsync(string id);

    /// <summary>
    /// Retrieves the basic medicines from the database.
    /// </summary>
    /// <returns>Returns all basic medicines from the database.</returns>
    Task<ICollection<BasicMedicineVM>> GetBasicMedicinesAsync();

    /// <summary>
    /// Refills the quantity of medicine in a pharmacy.
    /// </summary>
    /// <param name="quantity">The quantity.</param>
    /// <param name="pharmacyId">The ID of the pharmacy.</param>
    /// <param name="medicineId">The ID of the medicine.</param>
    /// <returns>A synchronous void task.</returns>
    Task RefillQuantityAsync(int quantity, string pharmacyId, string medicineId);
}
