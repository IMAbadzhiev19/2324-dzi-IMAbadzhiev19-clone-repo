namespace PMS.Shared.Models.User.Auth.Token;

/// <summary>
/// Represents an input model for JWT tokens.
/// </summary>
public class TokensIM
{
    /// <summary>
    /// Gets or sets the access token.
    /// </summary>
    public string AccessToken { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the refresh token.
    /// </summary>
    public string RefreshToken { get; set; } = string.Empty;
}