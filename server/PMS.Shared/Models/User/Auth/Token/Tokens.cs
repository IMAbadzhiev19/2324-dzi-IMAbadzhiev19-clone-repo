using System.IdentityModel.Tokens.Jwt;

namespace PMS.Shared.Models.User.Auth.Token;

/// <summary>
/// Represents a pair of JWT security tokens.
/// </summary>
public class Tokens
{
    /// <summary>
    /// Gets or sets the access token.
    /// </summary>
    public JwtSecurityToken? AccessToken { get; set; } = new ();

    /// <summary>
    /// Gets or sets the refresh token.
    /// </summary>
    public JwtSecurityToken? RefreshToken { get; set; } = new ();
}