using System.ComponentModel.DataAnnotations;

namespace PMS.Shared.Models.Medicines;

/// <summary>
/// Represents a basic medicine request.
/// </summary>
public class BasicMedicineRequest
{
    /// <summary>
    /// Gets or sets the name of the medicine.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the email of the recipient.
    /// </summary>
    [Required]
    public string Email { get; set; } = string.Empty;
}