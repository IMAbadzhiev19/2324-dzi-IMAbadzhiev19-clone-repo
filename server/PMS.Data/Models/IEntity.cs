namespace PMS.Data.Models;

/// <summary>
/// Represents an interface for entities with an ID.
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Gets or sets the ID of the entity.
    /// </summary>
    string Id { get; set; }
}