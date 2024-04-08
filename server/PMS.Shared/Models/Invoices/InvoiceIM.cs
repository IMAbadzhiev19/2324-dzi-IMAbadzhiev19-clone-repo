using System.ComponentModel.DataAnnotations;
using PMS.Shared.Models.Medicines;

namespace PMS.Shared.Models.Invoices;

/// <summary>
/// Represents an input model for an invoice.
/// </summary>
public class InvoiceIM
{
    /// <summary>
    /// Gets or sets the total price of the invoice.
    /// </summary>
    [Required]
    public decimal TotalPrice { get; set; }

    /// <summary>
    /// Gets or sets the ID of the pharmacist associated with the invoice.
    /// </summary>
    [Required]
    public string PharmacistId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the depot associated with the invoice.
    /// </summary>
    [Required]
    public string DepotId { get; set; }

    /// <summary>
    /// Gets or sets the ID of the pharmacy associated with the invoice.
    /// </summary>
    [Required]
    public string PharmacyId { get; set; }

    /// <summary>
    /// Gets or sets the collection of medicines associated with the invoice.
    /// </summary>
    public virtual ICollection<MedicineInvoiceModel> Medicines { get; set; } = new HashSet<MedicineInvoiceModel>();
}
