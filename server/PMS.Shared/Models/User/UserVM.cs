namespace PMS.Shared.Models.User;

/// <summary>
/// Represents a view model for user information.
/// </summary>
public class UserVM
{
    /// <summary>
    /// Gets or sets the ID of the user.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string FirstName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string LastName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the phone number of the user.
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    /// The worked hours by the user.
    /// </summary>
    public int WorkedHours { get; set; }

    /// <summary>
    /// Gets or sets the user ID of the creator of this entity.
    /// </summary>
    public string CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the date and time when this entity was created.
    /// </summary>
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// Gets or sets the user ID of the last updater of this entity.
    /// </summary>
    public string UpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets the date and time when this entity was last updated.
    /// </summary>
    public DateTime UpdatedOn { get; set; }
}
