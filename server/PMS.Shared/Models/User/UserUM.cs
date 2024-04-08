using Microsoft.AspNetCore.Http;
using PMS.Shared.DataAnnotations;

namespace PMS.Shared.Models.User;

/// <summary>
/// Represents an update model for user information.
/// </summary>
public class UserUM
{
    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public string? Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string? FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string? LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the phone number of the user.
    /// </summary>
    public string? PhoneNumber { get; set; } = string.Empty;
}