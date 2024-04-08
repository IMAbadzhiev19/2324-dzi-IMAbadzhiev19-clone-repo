using Azure.Communication.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PMS.Services.Contracts;
using PMS.Shared.Contracts;
using PMS.Shared.Options;

namespace PMS.Services.Implementations;

/// <summary>
/// A class responsible for email-related operations.
/// </summary>
internal class EmailService : IEmailService
{
    private readonly AzureEmailClientOptions options;

    public EmailService(IOptions<AzureEmailClientOptions> options)
    {
        this.options = options.Value;
    }

    /// <inheritdoc />
    public async Task SendEmailAsync(IEmailService.SendEmailRequest request)
    {
        EmailClient emailClient = new EmailClient(this.options.ConnectionString);
        EmailContent emailContent = new EmailContent(request.Subject);
        emailContent.PlainText = request.Message;
        emailContent.Html = request.Message;
        var emailRecipients = new EmailRecipients(new List<EmailAddress> { new EmailAddress(request.Recipient) });
        EmailMessage emailMessage = new EmailMessage("DoNotReply@9247c423-09bf-47ba-a2f5-3295c10eb303.azurecomm.net", emailRecipients, emailContent);

        if (request.FileContent is not null)
        {
            var bytes = Convert.FromBase64String(request.FileContent);
            var binaryData = new BinaryData(bytes);

            var attachment = new EmailAttachment($"{request.FileName}.pdf", "application/pdf", binaryData);
            emailMessage.Attachments.Add(attachment);
        }

        await emailClient.SendAsync(Azure.WaitUntil.Completed, emailMessage);
    }
}