using System.Runtime.CompilerServices;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Options;
using PMS.Services.Contracts;
using PMS.Shared.Options;

[assembly: InternalsVisibleTo("PMS.Tests")]

namespace PMS.Services.Implementations;

/// <summary>
/// A class responsible for file-related operations.
/// </summary>
internal class FileService : IFileService
{
    private readonly AzureStorageOptions options;
    private readonly BlobServiceClient blobServiceClient;

    /// <summary>
    /// Initializes a new instance of the <see cref="FileService"/> class.
    /// </summary>
    /// <param name="options">The options.</param>
    public FileService(IOptions<AzureStorageOptions> options)
    {
        this.options = options.Value;
        this.blobServiceClient = new BlobServiceClient(this.options.ConnectionString);
    }

    /// <inheritdoc />
    public async Task<string> UploadImageAsync(IFormFile image)
    {
        var containerName = this.options.ContainerName;
        var containerClient = this.blobServiceClient.GetBlobContainerClient(containerName);
        containerClient.CreateIfNotExists();

        BlockBlobClient blockBlobClient = containerClient.GetBlockBlobClient(
            Path.GetRandomFileName() + Guid.NewGuid().ToString() + Path.GetExtension(image.FileName).ToLowerInvariant());

        new FileExtensionContentTypeProvider().TryGetContentType(image.FileName, out var contentType);

        var blobHttpHeader = new BlobHttpHeaders { ContentType = (contentType ?? "application/octet-stream").ToLowerInvariant() };

        await blockBlobClient.UploadAsync(
            image.OpenReadStream(),
            new BlobUploadOptions { HttpHeaders = blobHttpHeader });

        return blockBlobClient.Uri.AbsoluteUri;
    }
}