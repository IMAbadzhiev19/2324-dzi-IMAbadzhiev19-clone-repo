using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Data.Models.Auth;
using PMS.Data.Models.PharmacyEntities;
using Quartz;

namespace PMS.Services.Implementations.BackgroundJobs;

/// <summary>
/// A job for working hours validation.
/// </summary>
public class WorkingHoursValidation : IJob
{
    private readonly ApplicationDbContext context;
    private readonly UserManager<ApplicationUser> userManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="WorkingHoursValidation"/> class.
    /// </summary>
    /// <param name="context">Application database context.</param>
    /// <param name="userManager">The user manager.</param>
    public WorkingHoursValidation(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        this.context = context;
        this.userManager = userManager;
    }

    /// <summary>
    /// Methods used to execute the background job.
    /// </summary>
    /// <param name="context">Job execution context.</param>
    /// <returns>A synchronous void task.</returns>
    public async Task Execute(IJobExecutionContext context)
    {
        var activities = await this.context.Activities.ToListAsync();

        foreach (var activity in activities)
        {
            var user = await this.userManager.FindByIdAsync(activity.UserId!);
            var workedHours = activity.LastMadeRequest.Hour - activity.FirstMadeRequest.Hour;

            user.WorkedHours += workedHours;
        }

        this.context.Activities.RemoveRange(activities);
        await this.context.SaveChangesAsync();
    }
}