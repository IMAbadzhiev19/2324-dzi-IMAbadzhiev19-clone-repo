using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Data.Models.PharmacyEntities;

namespace PMS.Data.Configurations;

/// <summary>
/// Configuration class for entity Activity.
/// </summary>
public class ActivityEntityConfiguration : IEntityTypeConfiguration<Activity>
{
    /// <summary>
    /// Configures the entity Activity.
    /// </summary>
    /// <param name="builder"></param>
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder
            .HasOne(a => a.User)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
    }
}