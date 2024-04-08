namespace PMS.Data.Models.PharmacyEntities;

/// <summary>
/// Represents a medicine entity.
/// </summary>
public class Medicine : AuditableEntity
{
    /// <summary>
    /// Gets or sets the price of the medicine.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the expiration date of the medicine.
    /// </summary>
    public DateOnly ExpirationDate { get; set; }

    /// <summary>
    /// Gets or sets the URL of the image associated with the medicine.
    /// </summary>
    public string ImageUrl { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the ID of the basic medicine associated with this medicine.
    /// </summary>
    public string BasicMedicineId { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the medicine.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the medicine is expired.
    /// </summary>
    public bool IsExpired { get; set; }

    /// <summary>
    /// Gets or sets the reference to the basic medicine associated with this medicine.
    /// </summary>
    public virtual BasicMedicine BasicMedicine { get; set; }
}