using PMS.Shared.Models.Invoices;

namespace PMS.Services.Contracts.Pharmacy;

/// <summary>
/// Interface for invoice service.
/// </summary>
public interface IInvoiceService
{
    /// <summary>
    /// Generates an invoice asynchronously.
    /// </summary>
    /// <param name="invoiceIM">The invoice input model.</param>
    /// <returns>A byte array representing the generated invoice.</returns>
    Task<byte[]> GenerateInvoiceAsync(InvoiceIM invoiceIM);
}