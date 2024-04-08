using System.ComponentModel.DataAnnotations;

namespace PMS.Shared.Models.Pharmacies;

/// <summary>
/// Represents an update model for pharmacy information.
/// </summary>
public class PharmacyUM
{
    /// <summary>
    /// Gets or sets the ID of the pharmacy.
    /// </summary>
    [Required]
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the pharmacy.
    /// </summary>
    public string? Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the pharmacy.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the address of the pharmacy.
    /// </summary>
    public Address? Address { get; set; } = default!;

    /// <summary>
    /// Gets or sets the ID of the founder of the pharmacy.
    /// </summary>
    public string? FounderId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the associated depot.
    /// </summary>
    public string? DepotId { get; set; }
}