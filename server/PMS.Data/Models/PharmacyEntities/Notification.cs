namespace PMS.Data.Models.PharmacyEntities;

/// <summary>
/// Represents a notification entity.
/// </summary>
public class Notification : BaseEntity
{
    /// <summary>
    /// Gets or sets the text content of the notification.
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the date on which the notification was sent.
    /// </summary>
    public DateOnly SentOn { get; set; }

    /// <summary>
    /// Gets or sets whether the notification is an assign request.
    /// </summary>
    public bool IsAssignRequest { get; set; }

    /// <summary>
    /// Gets or sets whether the notification is a warning request.
    /// </summary>
    public bool IsWarning { get; set; }

    /// <summary>
    /// Gets or sets the ID of the depot.
    /// </summary>
    public string? DepotId { get; set; }

    /// <summary>
    /// Gets or sets the reference of the depot associated with the notification.
    /// </summary>
    public virtual Depot? Depot { get; set; }

    /// <summary>
    /// Gets or sets the ID of the pharmacy.
    /// </summary>
    public string? PharmacyId { get; set; }

    /// <summary>
    /// Gets or sets the reference of the pharmacy associated with the notification.
    /// </summary>
    public virtual Pharmacy? Pharmacy { get; set; }
}