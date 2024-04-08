namespace PMS.Data.Models;

/// <summary>
/// Represents an abstract class for base entities.
/// </summary>
public abstract class BaseEntity : IEntity
{
    /// <summary>
    /// Gets or sets the ID of the entity.
    /// </summary>
    public string Id { get; set; }
}
