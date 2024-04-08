using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Data.Models.Auth;

namespace PMS.Data.Configurations;

/// <summary>
/// Configuration class for the entity ApplicationUser.
/// </summary>
public class UserEntityConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    /// <summary>
    /// Configures the entity ApplicationUser.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(u => u.Id);

        builder.Property(u => u.FirstName)
            .HasMaxLength(70)
            .IsRequired();

        builder.Property(u => u.LastName)
            .HasMaxLength(70)
            .IsRequired();
    }
}