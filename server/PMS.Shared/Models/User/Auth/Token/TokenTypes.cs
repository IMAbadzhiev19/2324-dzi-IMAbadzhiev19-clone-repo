namespace PMS.Shared.Models.User.Auth.Token;

/// <summary>
/// Enumerates the types of tokens.
/// </summary>
public enum TokenTypes
{
    /// <summary>
    /// Access token.
    /// </summary>
    AccessToken,

    /// <summary>
    /// Refresh token.
    /// </summary>
    RefreshToken,
}