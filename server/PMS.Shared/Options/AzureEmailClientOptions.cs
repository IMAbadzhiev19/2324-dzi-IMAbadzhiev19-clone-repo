namespace PMS.Shared.Options;

/// <summary>
/// Options pattern class representing the azure email client options from IConfiguration.
/// </summary>
public class AzureEmailClientOptions
{
    /// <summary>
    /// The name of the json object in IConfiguration.
    /// </summary>
    public const string AzureEmailClient = "Azure:EmailClient";

    /// <summary>
    /// Gets or sets the connection string.
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;
}
