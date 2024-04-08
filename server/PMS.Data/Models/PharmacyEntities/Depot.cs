using PMS.Data.Models.Auth;
using PMS.Shared.Models;

namespace PMS.Data.Models.PharmacyEntities;

/// <summary>
/// Represents a depot entity.
/// </summary>
public class Depot : AuditableEntity
{
    /// <summary>
    /// Gets or sets the name of the depot.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the ID of the manager associated with this depot.
    /// </summary>
    public string? ManagerId { get; set; }

    /// <summary>
    /// Gets or sets the address of the depot.
    /// </summary>
    public Address Address { get; set; }

    /// <summary>
    /// Gets or sets the manager associated with this depot.
    /// </summary>
    public virtual ApplicationUser? Manager { get; set; }

    /// <summary>
    /// Gets or sets the collection of medicines associated with this depot.
    /// </summary>
    public virtual ICollection<Medicine> Medicines { get; set; } = new HashSet<Medicine>();
}