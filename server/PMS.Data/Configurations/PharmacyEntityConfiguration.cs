using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Data.Models.PharmacyEntities;

namespace PMS.Data.Configurations;

/// <summary>
/// Configuration class for the entity Pharmacy.
/// </summary>
public class PharmacyEntityConfiguration : IEntityTypeConfiguration<Pharmacy>
{
    /// <summary>
    /// Configures the entity Pharmacy.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<Pharmacy> builder)
    {
        builder.HasKey(p => p.Id);

        builder
            .Property(p => p.Description)
            .IsRequired(false);

        builder
            .HasOne(p => p.Founder)
            .WithMany();

        builder
            .HasOne(p => p.Depot)
            .WithMany()
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasMany(p => p.Pharmacists)
            .WithOne();

        builder
            .HasMany(p => p.Medicines)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .OwnsOne(p => p.Address);
    }
}