using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Data.Models.PharmacyEntities;

namespace PMS.Data.Configurations;

/// <summary>
/// Configuration class for the entity Medicine.
/// </summary>
public class MedicineEntityConfiguration : IEntityTypeConfiguration<Medicine>
{
    /// <summary>
    /// Configures the entity Medicine.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<Medicine> builder)
    {
        builder.HasKey(ms => ms.Id);

        builder
            .HasOne(ms => ms.BasicMedicine)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .Property(ms => ms.Price)
            .HasPrecision(6, 2);
    }
}
