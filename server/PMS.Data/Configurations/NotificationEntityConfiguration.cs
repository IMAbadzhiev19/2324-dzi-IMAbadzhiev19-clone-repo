using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Data.Models.PharmacyEntities;

namespace PMS.Data.Configurations;

/// <summary>
/// Configuration class for the entity Notification.
/// </summary>
public class NotificationEntityConfiguration : IEntityTypeConfiguration<Notification>
{
    /// <summary>
    /// Configures the entity Notification.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<Notification> builder)
    {
        builder
            .HasKey(n => n.Id);

        builder
            .HasOne(n => n.Pharmacy)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(n => n.Depot)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);
    }
}