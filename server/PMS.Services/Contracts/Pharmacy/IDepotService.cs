using Essentials.Results;
using PMS.Shared.Models.Depots;

namespace PMS.Services.Contracts.Pharmacy;

/// <summary>
/// Interface for depot service.
/// </summary>
public interface IDepotService
{
    /// <summary>
    /// Creates a new depot asynchronously.
    /// </summary>
    /// <param name="depotIM">The depot input model.</param>
    /// <param name="managerId">The ID of the manager.</param>
    /// <returns>The ID of the newly created depot.</returns>
    Task<MutationResult> CreateDepotAsync(DepotIM depotIM, string managerId);

    /// <summary>
    /// Updates a depot asynchronously.
    /// </summary>
    /// <param name="depotUM">The depot update model.</param>
    /// <returns>A synchronous void task.</returns>
    Task UpdateDepotAsync(DepotUM depotUM);

    /// <summary>
    /// Deletes a depot asynchronously.
    /// </summary>
    /// <param name="id">The ID of the depot to delete.</param>
    /// <returns>A synchronous void task.</returns>
    Task DeleteDepotAsync(string id);

    /// <summary>
    /// Retrieves all depots asynchronously.
    /// </summary>
    /// <returns>A collection of depot view models.</returns>
    Task<ICollection<DepotVM>> GetAllDepotsAsync();

    /// <summary>
    /// Retrieves all depots owned by user with id asynchronously.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>A collection of depot view models.</returns>
    Task<ICollection<DepotVM>> GetDepotsByUserIdAsync(string userId);

    /// <summary>
    /// Retrieves a depot by ID asynchronously.
    /// </summary>
    /// <param name="id">The ID of the depot.</param>
    /// <returns>The depot view model.</returns>
    Task<DepotVM> GetDepotByIdAsync(string id);

    /// <summary>
    /// Assigns a depot to a pharmacy asynchronously.
    /// </summary>
    /// <param name="depotId">The ID of the depot.</param>
    /// <param name="pharmacyId">The ID of the pharmacy.</param>
    /// <returns>A synchronous void task.</returns>
    Task AssignDepotToPharmacyAsync(string depotId, string pharmacyId);
}