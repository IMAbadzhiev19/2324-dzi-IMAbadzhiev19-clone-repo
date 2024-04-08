namespace PMS.Data.Models;

/// <summary>
/// Represents an interface for auditable entities.
/// </summary>
public interface IAuditableEntity
{
    /// <summary>
    /// Gets or sets the user ID of the creator of this entity.
    /// </summary>
    string? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the date and time when this entity was created.
    /// </summary>
    DateTime CreatedOn { get; set; }

    /// <summary>
    /// Gets or sets the user ID of the last updater of this entity.
    /// </summary>
    string? UpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets the date and time when this entity was last updated.
    /// </summary>
    DateTime UpdatedOn { get; set; }
}