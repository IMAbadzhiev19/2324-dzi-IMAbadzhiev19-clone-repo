namespace PMS.Services.Contracts.Pharmacy;

/// <summary>
/// Interface for activity service.
/// </summary>
public interface IActivityService
{
    /// <summary>
    /// Creates activity for user when he fetches all medicines.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>An asynchronous result of the operation.</returns>
    Task CreateActivityForUserAsync(string userId);

    /// <summary>
    /// Changes the date and time of the last made request of the activity.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>An asynchronous result of the operation.</returns>
    Task ChangeLastRequestAsync(string userId);
}