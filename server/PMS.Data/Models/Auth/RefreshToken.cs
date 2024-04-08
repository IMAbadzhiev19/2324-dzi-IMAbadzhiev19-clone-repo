namespace PMS.Data.Models.Auth;

/// <summary>
/// Represents a refresh token entity used for authentication and authorization.
/// </summary>
public class RefreshToken : BaseEntity
{
    /// <summary>
    /// Gets or sets the refresh token value.
    /// </summary>
    public string Token { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the ID of the user associated with this refresh token.
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the reference to the ApplicationUser associated with this refresh token.
    /// </summary>
    public virtual ApplicationUser User { get; set; } = default!;
}