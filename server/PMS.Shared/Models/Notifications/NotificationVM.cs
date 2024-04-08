namespace PMS.Shared.Models.Notifications;

/// <summary>
/// Represents a view model for notification information.
/// </summary>
public class NotificationVM
{
    /// <summary>
    /// The ID of the notification.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// The text content of the notification.
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// The date of the notification sending.
    /// </summary>
    public DateOnly SentOn { get; set; }

    /// <summary>
    /// A prop representing whether the notification is an assign request or not.
    /// </summary>
    public bool IsAssignRequest { get; set; }

    /// <summary>
    /// A prop representing whether the notification is an warning or not.
    /// </summary>
    public bool IsWarning { get; set; }

    /// <summary>
    /// The ID of the pharmacy associated with the notification.
    /// </summary>
    public string? PharmacyId { get; set; } = string.Empty;

    /// <summary>
    /// The ID of the depot associated with the notification.
    /// </summary>
    public string? DepotId { get; set; } = string.Empty;
}