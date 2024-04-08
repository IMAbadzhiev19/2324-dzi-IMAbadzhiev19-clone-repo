namespace PMS.Shared.Models.Medicines;

/// <summary>
/// Represents a basic view model for medicine information.
/// </summary>
public class BasicMedicineVM
{
    /// <summary>
    /// Gets or sets the ID of the medicine.
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the medicine.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the medicine.
    /// </summary>
    public string? Description { get; set; }
}