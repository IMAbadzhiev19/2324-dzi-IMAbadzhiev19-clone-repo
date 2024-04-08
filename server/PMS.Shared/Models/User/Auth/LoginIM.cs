using System.ComponentModel.DataAnnotations;

namespace PMS.Shared.Models.User.Auth;

/// <summary>
/// Represents an input model for user login information.
/// </summary>
public class LoginIM
{
    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    [Required]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    [Required]
    public string Password { get; set; } = string.Empty;
}