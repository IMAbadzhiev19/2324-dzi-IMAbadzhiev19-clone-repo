namespace PMS.Shared.Options;

/// <summary>
/// Options pattern class representing the tokens options from IConfiguration.
/// </summary>
public class TokensOptions
{
    /// <summary>
    /// The name of the json object in IConfiguration.
    /// </summary>
    public const string Tokens = "Tokens";

    /// <summary>
    /// Gets or sets the access token secret key.
    /// </summary>
    public string AccessTokenSecret { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the refresh token secret key.
    /// </summary>
    public string RefreshTokenSecret { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the access token validity in minutes.
    /// </summary>
    public string AccessTokenValidityInMinutes { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the refresh token validity in days.
    /// </summary>
    public string RefreshTokenValidityInDays { get; set; } = string.Empty;
}