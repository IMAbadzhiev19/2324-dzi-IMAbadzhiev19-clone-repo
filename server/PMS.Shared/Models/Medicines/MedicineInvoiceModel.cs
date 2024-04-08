namespace PMS.Shared.Models.Medicines;

/// <summary>
/// An invoice medicine model.
/// </summary>
public class MedicineInvoiceModel
{
    /// <summary>
    /// Gets or sets the ID of the medicine.
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the medicine.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the price of the medicine.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Gets or sets the quantity of the medicine.
    /// </summary>
    public int Quantity { get; set; }
}
