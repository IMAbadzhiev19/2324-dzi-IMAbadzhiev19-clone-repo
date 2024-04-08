using PMS.Data.Models.Auth;

namespace PMS.Data.Models.PharmacyEntities;

/// <summary>
/// Represents an activity class used for tracking employees activities.
/// </summary>
public class Activity : BaseEntity
{
    /// <summary>
    /// Gets or sets the date and time when the user has made his/her first request for the day.
    /// </summary>
    public DateTime FirstMadeRequest { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the user has made his/her last request for the day.
    /// </summary>
    public DateTime LastMadeRequest { get; set; }

    /// <summary>
    /// Gets or sets the id of the user.
    /// </summary>
    public string? UserId { get; set; }

    /// <summary>
    /// Gets or sets user associated with the activity.
    /// </summary>
    public virtual ApplicationUser? User { get; set; }
}