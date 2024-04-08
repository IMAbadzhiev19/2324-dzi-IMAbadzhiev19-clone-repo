using System.ComponentModel.DataAnnotations;
using PMS.Shared.DataAnnotations;

namespace PMS.Shared.Models.Medicines;

/// <summary>
/// Represents an input model for medicine information.
/// </summary>
public class MedicineIM
{
    /// <summary>
    /// Gets or sets the ID of the medicine.
    /// </summary>
    [Required]
    public string BasicMedicineId { get; set; }

    /// <summary>
    /// Gets or sets the price of the medicine.
    /// </summary>
    [Required]
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the medicine.
    /// </summary>
    [Required]
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets the expiration date of the medicine. This property is ignored during adaptation.
    /// </summary>
    [DateOnly]
    public string? ExpirationDate { get; set; }
}