using PMS.Shared.Models.Depots;
using PMS.Shared.Models.Medicines;
using PMS.Shared.Models.User;

namespace PMS.Shared.Models.Pharmacies;

/// <summary>
/// Represents a view model for pharmacy information.
/// </summary>
public class PharmacyVM
{
    /// <summary>
    /// Gets or sets the ID of the pharmacy.
    /// </summary>
    public string Id { get; set; } = string.Empty;

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
    /// Gets or sets the founder of the pharmacy.
    /// </summary>
    public virtual UserVM Founder { get; set; }

    /// <summary>
    /// Gets or sets the depot associated with the pharmacy.
    /// </summary>
    public virtual DepotVM? Depot { get; set; }

    /// <summary>
    /// Gets or sets the collection of pharmacists associated with the pharmacy.
    /// </summary>
    public virtual ICollection<UserVM> Pharmacists { get; set; } = new HashSet<UserVM>();

    /// <summary>
    /// Gets or sets the collection of medicines associated with the pharmacy.
    /// </summary>
    public virtual ICollection<MedicineVM> Medicines { get; set; } = new HashSet<MedicineVM>();

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