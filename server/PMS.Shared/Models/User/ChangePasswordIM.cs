using System.ComponentModel.DataAnnotations;

namespace PMS.Shared.Models.User;

/// <summary>
/// An input model for changing password.
/// </summary>
public class ChangePasswordIM
{
    /// <summary>
    /// The old password of the user.
    /// </summary>
    [Required]
    public string OldPassword { get; set; } = string.Empty;

    /// <summary>
    /// The new password of the user.
    /// </summary>
    [Required]
    public string NewPassword { get; set; } = string.Empty;
}