using Essentials.Results;
using PMS.Shared.Contracts;
using PMS.Shared.Models.Pharmacies;
using PMS.Shared.Models.User;

namespace PMS.Services.Contracts.Pharmacy;

/// <summary>
/// Interface for pharmacy service.
/// </summary>
public interface IPharmacyService
{
    /// <summary>
    /// Creates a new pharmacy asynchronously.
    /// </summary>
    /// <param name="pharmacyIM">The pharmacy input model.</param>
    /// <param name="founderId">The id of the founder.</param>
    /// <returns>The ID of the newly created pharmacy.</returns>
    Task<MutationResult> CreatePharmacyAsync(PharmacyIM pharmacyIM, string founderId);

    /// <summary>
    /// Updates a pharmacy asynchronously.
    /// </summary>
    /// <param name="pharmacyUM">The pharmacy update model.</param>
    /// <returns>A synchronous void task.</returns>
    Task UpdatePharmacyAsync(PharmacyUM pharmacyUM);

    /// <summary>
    /// Deletes a pharmacy asynchronously.
    /// </summary>
    /// <param name="id">The ID of the pharmacy to delete.</param>
    /// <returns>A synchronous void task.</returns>
    Task DeletePharmacyAsync(string id);

    /// <summary>
    /// Retrieves a pharmacy by ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the pharmacy.</param>
    /// <returns>The pharmacy view model.</returns>
    Task<PharmacyVM> GetPharmacyByIdAsync(string id);

    /// <summary>
    /// Retrieves all pharmacies owned by user asynchronously.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>A collection of pharmacy view models.</returns>
    Task<ICollection<PharmacyVM>> GetPharmaciesAsync(string userId);

    /// <summary>
    /// Checks whether a user has any pharmacies he/she if founder of.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>Whether it is true of false.</returns>
    Task<bool> CheckIfUserHasAnyPharmacies(string userId);

    /// <summary>
    /// Assigns employee to pharmacy.
    /// </summary>
    /// <param name="pharmacyId">The ID of the pharmacy.</param>
    /// <param name="email">The email of the employee.</param>
    /// <returns>The result of the operation.</returns>
    Task AssignEmployeeAsync(string pharmacyId, string email);

    /// <summary>
    /// Removes employee from pharmacy.
    /// </summary>
    /// <param name="pharmacyId">The ID of the pharmacy.</param>
    /// <param name="employeeId">The ID of the employee.</param>
    /// <returns>The result of the operation.</returns>
    Task RemoveEmployeeAsync(string pharmacyId, string employeeId);

    /// <summary>
    /// Retrieves all pharmacies working in a pharmacy.
    /// </summary>
    /// <param name="pharmacyId">The ID of the pharmacy.</param>
    /// <returns>A collection containing all the pharmacists in the pharmacy.</returns>
    Task<ICollection<UserVM>> GetPharmacistsAsync(string pharmacyId);

    /// <summary>
    /// Retrieves all pharmacies by depot ID asynchronously.
    /// </summary>
    /// <param name="depotId">The ID of the depot.</param>
    /// <returns>A collection of pharmacy view models.</returns>
    Task<ICollection<PharmacyVM>> GetPharmaiesByDepotId(string depotId);

    /// <summary>
    /// Requests depot assign.
    /// </summary>
    /// <param name="pharmacyId">The ID of the pharmacy.</param>
    /// <param name="depotId">The ID of the depot.</param>
    /// <returns>The result of the operation.</returns>
    Task RequestDepotAssignAsync(string pharmacyId, string depotId);
}