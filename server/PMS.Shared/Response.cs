using System.ComponentModel.DataAnnotations;

namespace PMS.Shared;

/// <summary>
/// Represents a response.
/// </summary>
public class Response
{
    /// <summary>
    /// Gets or sets the status of the response.
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the message of the response.
    /// </summary>
    public string Message { get; set; } = string.Empty;
}
