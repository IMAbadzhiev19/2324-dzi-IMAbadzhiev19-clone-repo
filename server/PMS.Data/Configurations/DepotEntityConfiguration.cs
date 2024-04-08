using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Data.Models.PharmacyEntities;

namespace PMS.Data.Configurations;

/// <summary>
/// Configuration class for the entity Depot.
/// </summary>
public class DepotEntityConfiguration : IEntityTypeConfiguration<Depot>
{
    /// <summary>
    /// Configures the entity Depot.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<Depot> builder)
    {
        builder.HasKey(d => d.Id);

        builder
            .HasOne(d => d.Manager)
            .WithMany();

        builder
            .HasMany(d => d.Medicines)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .OwnsOne(d => d.Address);
    }
}
