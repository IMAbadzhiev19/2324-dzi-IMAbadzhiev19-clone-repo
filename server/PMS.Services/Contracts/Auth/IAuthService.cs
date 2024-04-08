using Essentials.Results;
using PMS.Shared.Models.User.Auth;

namespace PMS.Services.Contracts.Auth;

/// <summary>
/// Interface for authentication service.
/// </summary>
public interface IAuthService
{
    /// <summary>
    /// Creates a new user asynchronously.
    /// </summary>
    /// <param name="registerIM">The registration input model.</param>
    /// <returns>The result of the operation containing the message, status code and ID.</returns>
    Task<MutationResult> CreateUserAsync(RegisterIM registerIM);

    /// <summary>
    /// Checks if a user with the given email exists asynchronously.
    /// </summary>
    /// <param name="email">The email of the user to check.</param>
    /// <returns>True if the user exists, otherwise false.</returns>
    Task<bool> CheckIfUserExistsAsync(string email);

    /// <summary>
    /// Adds a role to a user asynchronously.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="roleType">The type of the role to add.</param>
    /// <returns>A synchronous void task.</returns>
    Task AddRoleToUserAsync(string userId, string roleType);

    /// <summary>
    /// Removes a role from a user asynchronously.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <param name="roleType">The type of the role to add.</param>
    /// <returns></returns>
    Task RemoveRoleFromUserAsync(string userId, string roleType);

    /// <summary>
    /// Checks if the provided password is correct for the given email asynchronously.
    /// </summary>
    /// <param name="email">The email of the user.</param>
    /// <param name="password">The password to check.</param>
    /// <returns>True if the password is correct, otherwise false.</returns>
    Task<bool> CheckPasswordAsync(string email, string password);
}