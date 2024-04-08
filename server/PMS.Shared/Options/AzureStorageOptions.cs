namespace PMS.Shared.Options;

/// <summary>
/// Options pattern class representing the azure storage options from IConfiguration.
/// </summary>
public class AzureStorageOptions
{
    /// <summary>
    /// The name of the json object in IConfiguration.
    /// </summary>
    public const string AzureStorage = "Azure:Storage";

    /// <summary>
    /// Gets or sets the name of the container.
    /// </summary>
    public string ContainerName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the connection string.
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;
}