using Microsoft.AspNetCore.Identity;
using PMS.Shared.Models.User;

namespace PMS.Services.Contracts.Auth;

/// <summary>
/// Interface for user service.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Retrieves user information by ID asynchronously.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>The user view model.</returns>
    Task<UserVM> GetUserByIdAsync(string userId);

    /// <summary>
    /// Updates user information asynchronously.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <param name="userUM">The user update model.</param>
    /// <returns>A synchronous void task.</returns>
    Task UpdateUserInfoAsync(string id, UserUM userUM);

    /// <summary>
    /// Changes the password of a user asynchronously.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="newPassword">The new password.</param>
    /// <returns>An <see cref="IdentityResult"/> representing the result of the operation.</returns>
    Task<IdentityResult> ChangePasswordAsync(string userId, string newPassword);
}