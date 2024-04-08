using Microsoft.AspNetCore.Http;

namespace PMS.Services.Contracts;

/// <summary>
/// Interface for file service.
/// </summary>
public interface IFileService
{
    /// <summary>
    /// Uploads image to the blob container.
    /// </summary>
    /// <param name="image">The image.</param>
    /// <returns>The url of the image.</returns>
    Task<string> UploadImageAsync(IFormFile image);
}