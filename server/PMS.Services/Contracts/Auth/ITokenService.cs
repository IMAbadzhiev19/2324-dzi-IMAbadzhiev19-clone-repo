using PMS.Data.Models.Auth;
using PMS.Shared.Models.User.Auth.Token;

namespace PMS.Services.Contracts.Auth;

/// <summary>
/// Interface for token service.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Creates tokens for a user asynchronously.
    /// </summary>
    /// <param name="email">The email of the user.</param>
    /// <returns>The tokens generated.</returns>
    Task<Tokens> CreateTokenForUserAsync(string email);

    /// <summary>
    /// Creates new tokens asynchronously.
    /// </summary>
    /// <param name="tokens">The tokens input model.</param>
    /// <returns>The new tokens generated.</returns>
    Task<Tokens> CreateNewTokensAsync(TokensIM tokens);

    /// <summary>
    /// Saves a refresh token asynchronously.
    /// </summary>
    /// <param name="refreshToken">The refresh token to save.</param>
    /// <returns>A synchronous void task.</returns>
    Task SaveRefreshTokenAsync(RefreshToken refreshToken);

    /// <summary>
    /// Deletes a refresh token asynchronously.
    /// </summary>
    /// <param name="userId">The ID of the user whose refresh token should be deleted.</param>
    /// <returns>A synchronous void task.</returns>
    Task DeleteRefreshTokenAsync(string userId);
}