using System.ComponentModel.DataAnnotations;

namespace PMS.Shared.Models.Depots;

/// <summary>
/// Represents an input model for depot information.
/// </summary>
public class DepotIM
{
    /// <summary>
    /// Gets or sets the name of the depot.
    /// </summary>
    [Required]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the address of the depot.
    /// </summary>
    [Required]
    public Address Address { get; set; }
}