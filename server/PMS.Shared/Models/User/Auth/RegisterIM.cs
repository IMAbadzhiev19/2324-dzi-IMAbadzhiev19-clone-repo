using System.ComponentModel.DataAnnotations;

namespace PMS.Shared.Models.User.Auth;

/// <summary>
/// Represents an input model for user registration information.
/// </summary>
public class RegisterIM
{
    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    [Required]
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    [Required]
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    [Required]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the phone number of the user.
    /// </summary>
    [Required]
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the password of the user. This property is ignored during adaptation.
    /// </summary>
    [Required]
    public string Password { get; set; } = string.Empty;
}