using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PMS.Shared.Models.Medicines;

/// <summary>
/// Medicine update model.
/// </summary>
public class MedicineUM
{
    /// <summary>
    /// Gets or sets the ID of the medicine.
    /// </summary>
    [Required]
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the price of the medicine.
    /// </summary>
    public decimal? Price { get; set; }

    /// <summary>
    /// Gets or sets the count of the medicine.
    /// </summary>
    public int? Count { get; set; }

    /// <summary>
    /// Gets or sets the URL of the image associated with the medicine.
    /// </summary>
    public IFormFile? Image { get; set; }
}