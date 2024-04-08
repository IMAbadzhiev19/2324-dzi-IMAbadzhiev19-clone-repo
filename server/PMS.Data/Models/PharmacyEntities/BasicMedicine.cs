namespace PMS.Data.Models.PharmacyEntities;

/// <summary>
/// Represents a basic medicine entity.
/// </summary>
public class BasicMedicine : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the basic medicine.
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the description of the basic medicine.
    /// </summary>
    public string? Description { get; set; }
}