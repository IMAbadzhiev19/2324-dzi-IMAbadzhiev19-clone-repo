namespace PMS.Shared.Models;

/// <summary>
/// Represents an address.
/// </summary>
public class Address
{
    /// <summary>
    /// Gets or sets the street number.
    /// </summary>
    public int? Number { get; set; }

    /// <summary>
    /// Gets or sets the street name.
    /// </summary>
    public string Street { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the city.
    /// </summary>
    public string City { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the country.
    /// </summary>
    public string Country { get; set; } = string.Empty;
}
