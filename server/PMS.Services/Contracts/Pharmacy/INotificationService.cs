using PMS.Services.Enums;
using PMS.Shared.Models.Notifications;

namespace PMS.Services.Contracts.Pharmacy;

/// <summary>
/// Interface for notification service.
/// </summary>
public interface INotificationService
{
    /// <summary>
    /// Retrieves notifications for asynchronously.
    /// </summary>
    /// <param name="pharmacyId">The ID of the pharmacy.</param>
    /// <param name="depotId">The ID of the depot.</param>
    /// <returns>A collection of notification view models.</returns>
    Task<ICollection<NotificationVM>> GetNotificationsAsync(string pharmacyId, string depotId);

    /// <summary>
    /// Retrieves all notifications which are assign request asynchronously.
    /// </summary>
    /// <param name="depotId">The ID of the depot.</param>
    /// <returns>A collection of notification view models.</returns>
    Task<ICollection<NotificationVM>> GetAssignRequestsAsync(string depotId);

    /// <summary>
    /// Retrieves all notifications which are only for depot asynchronously.
    /// </summary>
    /// <param name="depotId">The ID of the depot.</param>
    /// <returns>A collection of notification view models.</returns>
    Task<ICollection<NotificationVM>> GetNotificationsForDepotAsync(string depotId);

    /// <summary>
    /// Retrieves all notifications which are only for pharmacy asynchronously.
    /// </summary>
    /// <param name="pharmacyId">The ID of the pharmacy.</param>
    /// <returns>A collection of notification view models.</returns>
    Task<ICollection<NotificationVM>> GetNotificationsForPharmacyAsync(string pharmacyId);

    /// <summary>
    /// Retrieves all notifications which are warnings.
    /// </summary>
    /// <param name="buildingId">The ID of the building.</param>
    /// <param name="buildingType">The type of the building.</param>
    /// <returns>A collection of notification view models.</returns>
    Task<ICollection<NotificationVM>> GetWarningsAsync(string buildingId, BuildingType buildingType);
}