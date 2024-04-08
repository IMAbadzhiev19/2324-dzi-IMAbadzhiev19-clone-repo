using HandlebarsDotNet;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Data.Models.Auth;
using PMS.Data.Models.PharmacyEntities;
using PMS.Services.Contracts.Pharmacy;
using PMS.Shared.Constants;
using PMS.Shared.Models.Invoices;

namespace PMS.Services.Implementations.Pharmacy;

/// <summary>
/// A class responsible for invoice-related operations.
/// </summary>
internal class InvoiceService : IInvoiceService
{
    private readonly ApplicationDbContext context;
    private readonly UserManager<ApplicationUser> userManager;

    public InvoiceService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        this.context = context;
        this.userManager = userManager;
    }

    public async Task<byte[]> GenerateInvoiceAsync(InvoiceIM invoiceIM)
    {
        var content = InvoiceTemplate.Content;
        var handlebars = Handlebars.Compile(content);

        var pharmacist = await this.userManager.FindByIdAsync(invoiceIM.PharmacistId);
        var depot = await this.context.Depots
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == invoiceIM.DepotId);

        var medicines = this.context.Medicines
            .Include(m => m.BasicMedicine)
            .Where(m => invoiceIM.Medicines.Select(x => x.Id).Contains(m.Id))
            .ToArray();

        var pharmacy = await this.context.Pharmacies
            .Include(p => p.Founder)
            .FirstOrDefaultAsync(p => p.Id == invoiceIM.PharmacyId);

        // When we generate invoices, the quantity of each medicines is reduces.
        foreach (var medicine in medicines)
        {
            medicine.Quantity -= invoiceIM.Medicines.FirstOrDefault(m => m.Id == medicine.Id).Quantity;

            if (medicine.Quantity == 0)
            {
                var notification = new Notification
                {
                    Id = Guid.NewGuid().ToString(),
                    Text = $"Наличността на лекарство {medicine.BasicMedicine.Name} е нула в аптека: {pharmacy.Name}",
                    SentOn = DateOnly.FromDateTime(DateTime.UtcNow),
                    IsAssignRequest = false,
                    PharmacyId = invoiceIM.PharmacyId,
                    DepotId = invoiceIM.DepotId,
                    IsWarning = true,
                };

                await this.context.Notifications.AddAsync(notification);
            }
        }

        var quantities = invoiceIM.Medicines.Select(m => m.Quantity).ToList();

        var data = new
        {
            InvoiceNumber = Guid.NewGuid().ToString(),
            TotalPrice = invoiceIM.TotalPrice,
            FullName = $"{pharmacy.Founder.FirstName} {pharmacy.Founder.LastName}",
            Depot = depot,
            Medicines = invoiceIM.Medicines,
            PharmacyName = pharmacy.Name,
            Address = $"{pharmacy.Address.Street} {pharmacy.Address.Number}, {pharmacy.Address.City} {pharmacy.Address.Country}",
            Date = DateOnly.FromDateTime(DateTime.Now).ToString(),
        };

        var html = handlebars(data);

        var htmlConverter = new NReco.PdfGenerator.HtmlToPdfConverter();
        var pdfBytes = htmlConverter.GeneratePdf(html);

        // At the end so if error occurres during the pdf generation, the medicine quantity ain't reduced.
        await this.context.SaveChangesAsync(); // We know that the medicines were successfully sold when the save changes has succeeded.

        return pdfBytes;
    }
}