namespace PMS.Services.Contracts;

/// <summary>
/// Interface for email service.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// Sends an email.
    /// </summary>
    /// <param name="request">The request for the email.</param>
    /// <returns>A response containing the result.</returns>
    Task SendEmailAsync(SendEmailRequest request);

    /// <summary>
    /// A class representing the request for sending emails.
    /// </summary>
    public class SendEmailRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SendEmailRequest"/> class.
        /// </summary>
        /// <param name="recipient">The email of the recipient.</param>
        /// <param name="subject">The subject of the email.</param>
        /// <param name="message">The message of the email.</param>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="fileContent">The content of the file.</param>
        public SendEmailRequest(string recipient, string subject, string message, string? fileName, string? fileContent)
        {
            this.Recipient = recipient;
            this.Subject = subject;
            this.Message = message;
            this.FileName = fileName;
            this.FileContent = fileContent;
        }

        /// <summary>
        /// Gets the email of the recipient.
        /// </summary>
        public string Recipient { get; } = string.Empty;

        /// <summary>
        /// Gets subject of the email.
        /// </summary>
        public string Subject { get; } = string.Empty;

        /// <summary>
        /// Gets message to be sent.
        /// </summary>
        public string Message { get; } = string.Empty;

        /// <summary>
        /// Gets the name of the file to be send.
        /// </summary>
        public string? FileName { get; } = string.Empty;

        /// <summary>
        /// Gets the content of the file to be send.
        /// </summary>
        public string? FileContent { get; } = string.Empty;
    }
}