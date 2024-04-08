using System.ComponentModel.DataAnnotations;

namespace PMS.Shared.Models.Depots;

/// <summary>
/// Represents an update model for depot information.
/// </summary>
public class DepotUM
{
    /// <summary>
    /// Gets or sets the ID of the depot.
    /// </summary>
    [Required]
    public string Id { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the depot.
    /// </summary>
    public string? Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the manager ID of the depot.
    /// </summary>
    public string? ManagerId { get; set; }

    /// <summary>
    /// Gets or sets the address of the depot.
    /// </summary>
    public Address? Address { get; set; }
}