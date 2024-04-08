namespace PMS.Shared.Models;

/// <summary>
/// Input model for sharing resumes.
/// </summary>
public class ShareIM
{
    /// <summary>
    /// The email of the recipient.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// The file represented in base64 format.
    /// </summary>
    public string? Base64File { get; set; }

    /// <summary>
    /// The file name.
    /// </summary>
    public string? FileName { get; set; }
}