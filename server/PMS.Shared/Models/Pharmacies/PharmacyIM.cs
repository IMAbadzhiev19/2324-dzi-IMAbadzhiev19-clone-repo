using System.ComponentModel.DataAnnotations;

namespace PMS.Shared.Models.Pharmacies;

/// <summary>
/// Represents an input model for pharmacy information.
/// </summary>
public class PharmacyIM
{
    /// <summary>
    /// Gets or sets the name of the pharmacy.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the pharmacy.
    /// </summary>
    [Required]
    public string? Description { get; set; }

    /// <summary>
    /// Gets or sets the address of the pharmacy.
    /// </summary>
    [Required]
    public Address Address { get; set; } = default!;

    /// <summary>
    /// Gets or sets the ID of the associated depot.
    /// </summary>
    public string? DepotId { get; set; }
}