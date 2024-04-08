using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Services.Contracts;
using PMS.Services.Contracts.Pharmacy;
using PMS.Shared.Contracts;
using PMS.Shared.Models;
using PMS.Shared.Models.Invoices;

namespace PMS.WebHost.Controllers;

/// <summary>
/// Controller for handling invoice-related operations.
/// </summary>
[ApiController]
[Route("api/invoice")]
//[EnableRateLimiting("fixed")]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService invoiceService;
    private readonly IEmailService emailService;
    private readonly IActivityService activityService;
    private readonly ICurrentUser currentUser;

    /// <summary>
    /// Initializes a new instance of the <see cref="InvoiceController"/> class.
    /// </summary>
    /// <param name="invoiceService">The invoice service.</param>
    /// <param name="emailService">The email service.</param>
    /// <param name="activityService">The activity service.</param>
    /// <param name="currentUser">The current user service.</param>
    public InvoiceController(IInvoiceService invoiceService, IEmailService emailService, IActivityService activityService, ICurrentUser currentUser)
    {
        this.invoiceService = invoiceService;
        this.emailService = emailService;
        this.activityService = activityService;
        this.currentUser = currentUser;
    }

    /// <summary>
    /// Generates an invoice.
    /// </summary>
    /// <param name="invoiceIM">The invoice input model.</param>
    /// <returns>The result of the invoice generation operation.</returns>
    [HttpPost("generate")]
    [Authorize]
    public async Task<IActionResult> GenerateInvoiceAsync([FromBody] InvoiceIM invoiceIM)
    {
        var invoice = await this.invoiceService.GenerateInvoiceAsync(invoiceIM);
        await this.activityService.ChangeLastRequestAsync(this.currentUser.UserId);
        return this.Ok(Convert.ToBase64String(invoice));
    }

    /// <summary>
    /// Shares invoice.
    /// </summary>
    /// <param name="shareIM">The share input model.</param>
    /// <returns>The result of sharing an invoice.</returns>
    [HttpPost("share")]
    [Authorize]
    public async Task<IActionResult> ShareInvoiceAsync([FromBody] ShareIM shareIM)
    {
        var emailRequest = new IEmailService.SendEmailRequest(shareIM.Email, "New invoice", "Hello, \r\n Check out this invoice!", shareIM.FileName ?? null, shareIM.Base64File ?? null);
        await this.emailService.SendEmailAsync(emailRequest);
        await this.activityService.ChangeLastRequestAsync(this.currentUser.UserId);
        return this.Ok();
    }
}