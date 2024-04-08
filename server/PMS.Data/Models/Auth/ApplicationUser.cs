using Microsoft.AspNetCore.Identity;

namespace PMS.Data.Models.Auth;

/// <summary>
/// Represents an application user entity, inheriting from IdentityUser for authentication purposes.
/// </summary>
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the URL of the user's profile image.
    /// </summary>
    public string ImageUrl { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the worked hours of the user.
    /// </summary>
    public int WorkedHours { get; set; }
}