using PMS.Shared.Models.Medicines;
using PMS.Shared.Models.User;

namespace PMS.Shared.Models.Depots;

/// <summary>
/// Represents a view model for depot information.
/// </summary>
public class DepotVM
{
    /// <summary>
    /// Gets or sets the ID of the depot.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the depot.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the address of the depot.
    /// </summary>
    public Address Address { get; set; }

    /// <summary>
    /// Gets or sets the manager of the depot.
    /// </summary>
    public virtual UserVM Manager { get; set; }

    /// <summary>
    /// Gets or sets the collection of medicines in the depot.
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