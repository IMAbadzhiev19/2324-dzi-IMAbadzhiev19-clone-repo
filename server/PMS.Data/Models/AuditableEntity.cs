namespace PMS.Data.Models;

/// <summary>
/// Represents an abstract class for auditable entities.
/// </summary>
public abstract class AuditableEntity : BaseEntity, IAuditableEntity
{
    /// <summary>
    /// Gets or sets the user ID of the creator of this entity.
    /// </summary>
    public string? CreatedBy { get; set; }

    /// <summary>
    /// Gets or sets the date and time when this entity was created.
    /// </summary>
    public DateTime CreatedOn { get; set; }

    /// <summary>
    /// Gets or sets the user ID of the last updater of this entity.
    /// </summary>
    public string? UpdatedBy { get; set; }

    /// <summary>
    /// Gets or sets the date and time when this entity was last updated.
    /// </summary>
    public DateTime UpdatedOn { get; set; }
}