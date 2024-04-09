using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PMS.Data.Models;
using PMS.Data.Models.Auth;
using PMS.Data.Models.PharmacyEntities;
using PMS.Shared.Contracts;

namespace PMS.Data;

/// <summary>
/// Represents the application database context.
/// </summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    private readonly ICurrentUser currentUser;

    private readonly EntityState[] auditableStates =
    {
        EntityState.Added,
        EntityState.Modified,
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
    /// </summary>
    /// /// <param name="options">The options for this context.</param>
    /// <param name="currentUser">The current user service.</param>
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUser currentUser)
        : base(options)
    {
        this.currentUser = currentUser;
    }

    /// <summary>
    /// Gets or sets the set of refresh tokens.
    /// </summary>
    public virtual DbSet<RefreshToken> RefreshTokens { get; set; } = default!;

    /// <summary>
    /// Gets or sets the set of depots.
    /// </summary>
    public virtual DbSet<Depot> Depots { get; set; } = default!;

    /// <summary>
    /// Gets or sets the set of basic medicines.
    /// </summary>
    public virtual DbSet<BasicMedicine> BasicMedicines { get; set; } = default!;

    /// <summary>
    /// Gets or sets the set of pharmacies.
    /// </summary>
    public virtual DbSet<Pharmacy> Pharmacies { get; set; } = default!;

    /// <summary>
    /// Gets or sets the set of medicines.
    /// </summary>
    public virtual DbSet<Medicine> Medicines { get; set; } = default!;

    /// <summary>
    /// Gets or sets the set of notifications.
    /// </summary>
    public virtual DbSet<Notification> Notifications { get; set; } = default!;

    /// <summary>
    /// Gets or sets the set of activities.
    /// </summary>
    public virtual DbSet<Activity> Activities { get; set; } = default!;

    /// <summary>
    /// Overrides the SaveChanges method to handle auditable entities.
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess">A variable specifying whether all changes are accepted on success.</param>
    /// <returns>A result representing the saving operation.</returns>
    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        this.HandleAuditableEntities();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    /// <summary>
    /// Overrides the SaveChanges method to handle auditable entities.
    /// </summary>
    /// <returns>A result representing the saving operation.</returns>
    public override int SaveChanges()
    {
        this.HandleAuditableEntities();
        return base.SaveChanges();
    }

    /// <summary>
    /// Overrides the SaveChangesAsync method to handle auditable entities.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing a synchronous saving operation.</returns>
    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        this.HandleAuditableEntities();
        return base.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Overrides the SaveChangesAsync method to handle auditable entities.
    /// </summary>
    /// <param name="acceptAllChangesOnSuccess">A variable specifying whether all changes are accepted on success.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A task representing a synchronous saving operation.</returns>
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        this.HandleAuditableEntities();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    /// <summary>
    /// Configures the model using the specified builder.
    /// </summary>
    /// <param name="builder">The model builder.</param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
        builder.UseCollation("Cyrillic_General_CI_AS");

        base.OnModelCreating(builder);
    }

    /// <summary>
    /// Handles auditable entities to set creation and update information.
    /// </summary>
    private void HandleAuditableEntities()
    {
        var userId = this.currentUser?.UserId?.ToString();
        var now = DateTime.UtcNow;
        var auditableEntries = this.ChangeTracker
            .Entries()
            .Where(x => x.Entity is IAuditableEntity &&
                        this.auditableStates.Contains(x.State))
            .ToList();

        foreach (var entry in auditableEntries)
        {
            var entity = entry.Entity as IAuditableEntity;
            entity.UpdatedOn = now;
            entity.UpdatedBy = userId;

            if (entry.State == EntityState.Added)
            {
                entity.CreatedOn = now;
                entity.CreatedBy = userId;
            }
        }
    }
}