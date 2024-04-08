using PMS.Data.Models.Auth;
using PMS.Shared.Models;

namespace PMS.Data.Models.PharmacyEntities;

/// <summary>
/// Represents a pharmacy entity.
/// </summary>
public class Pharmacy : AuditableEntity
{
    /// <summary>
    /// Gets or sets the name of the pharmacy.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the pharmacy.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the address of the pharmacy.
    /// </summary>
    public Address Address { get; set; } = default!;

    /// <summary>
    /// Gets or sets the ID of the founder associated with this pharmacy.
    /// </summary>
    public string FounderId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the depot associated with this pharmacy.
    /// </summary>
    public string? DepotId { get; set; }

    /// <summary>
    /// Gets or sets the founder associated with this pharmacy.
    /// </summary>
    public virtual ApplicationUser Founder { get; set; }

    /// <summary>
    /// Gets or sets the depot associated with this pharmacy.
    /// </summary>
    public virtual Depot? Depot { get; set; }

    /// <summary>
    /// Gets or sets the collection of pharmacists associated with this pharmacy.
    /// </summary>
    public virtual ICollection<ApplicationUser> Pharmacists { get; set; } = new HashSet<ApplicationUser>();

    /// <summary>
    /// Gets or sets the collection of medicines associated with this pharmacy.
    /// </summary>
    public virtual ICollection<Medicine> Medicines { get; set; } = new HashSet<Medicine>();
}