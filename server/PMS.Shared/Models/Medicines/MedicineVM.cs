namespace PMS.Shared.Models.Medicines;

/// <summary>
/// Represents a view model for medicine information.
/// </summary>
public class MedicineVM
{
    /// <summary>
    /// Gets or sets the ID of the medicine.
    /// </summary>
    public string Id { get; set; } = string.Empty;

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
    /// Gets or sets the quantity of the medicine.
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the medicine is expired.
    /// </summary>
    public bool IsExpired { get; set; }

    /// <summary>
    /// Gets or sets the basic information of the medicine.
    /// </summary>
    public virtual BasicMedicineVM BasicMedicine { get; set; }

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