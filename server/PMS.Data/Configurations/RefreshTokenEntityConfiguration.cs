using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PMS.Data.Models.Auth;

namespace PMS.Data.Configurations;

/// <summary>
/// Configuration class for the entity RefreshToken.
/// </summary>
public class RefreshTokenEntityConfiguration : IEntityTypeConfiguration<RefreshToken>
{
    /// <summary>
    /// Configures the entity RefreshToken.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(rt => rt.Id);

        builder
            .HasOne(rt => rt.User)
            .WithOne();
    }
}