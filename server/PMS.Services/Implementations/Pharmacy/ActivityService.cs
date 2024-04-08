using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Data.Models.Auth;
using PMS.Data.Models.PharmacyEntities;
using PMS.Services.Contracts.Pharmacy;

namespace PMS.Services.Implementations.Pharmacy;

/// <summary>
/// A class responsible for activity-related operations.
/// </summary>
internal class ActivityService : IActivityService
{
    private readonly ApplicationDbContext context;
    private readonly UserManager<ApplicationUser> userManager;

    public ActivityService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        this.context = context;
        this.userManager = userManager;
    }

    public async Task ChangeLastRequestAsync(string userId)
    {
        if (!(await this.CheckWhetherUserIsPharmacist(userId)))
        {
            return;
        }

        var activity = await this.context.Activities
            .FirstOrDefaultAsync(a => a.UserId == userId
                && DateOnly.FromDateTime(a.FirstMadeRequest) == DateOnly.FromDateTime(DateTime.UtcNow));

        activity.LastMadeRequest = DateTime.UtcNow;

        await this.context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task CreateActivityForUserAsync(string userId)
    {
        if (this.CheckForActivityToday(userId) || !(await this.CheckWhetherUserIsPharmacist(userId)))
        {
            return;
        }

        var activity = new Activity
        {
            Id = Guid.NewGuid().ToString(),
            FirstMadeRequest = DateTime.UtcNow,
            LastMadeRequest = DateTime.UtcNow,
            UserId = userId,
        };

        await this.context.Activities.AddAsync(activity);
        await this.context.SaveChangesAsync();
    }

    /// <summary>
    /// Checks whether there is already activity created for the user today.
    /// </summary>
    /// <param name="userId"></param>
    /// <returns>A boolean result of the operation.</returns>
    private bool CheckForActivityToday(string userId)
    {
        var activity = this.context.Activities
            .Where(a => a.UserId == userId
                && DateOnly.FromDateTime(a.FirstMadeRequest) == DateOnly.FromDateTime(DateTime.UtcNow))
            .FirstOrDefault();

        return activity != null;
    }

    /// <summary>
    /// Checks whether user has a role of pharmacist.
    /// </summary>
    /// <param name="userId">The ID of the user.</param>
    /// <returns>A boolean result of the operation.</returns>
    private async Task<bool> CheckWhetherUserIsPharmacist(string userId)
    {
        var user = await this.userManager.FindByIdAsync(userId);
        if (user is null)
        {
            throw new ArgumentException("Невалидно \"id\" на потребителя");
        }

        var roles = await this.userManager.GetRolesAsync(user);
        return roles.Contains(UserRoles.Pharmacist);
    }
}