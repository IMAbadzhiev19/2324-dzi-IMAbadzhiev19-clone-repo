using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Data.Models.PharmacyEntities;
using Quartz;

namespace PMS.Services.Implementations.BackgroundJobs;

/// <summary>
/// A job for expiration date of medicines validation.
/// </summary>
public class ExpirationDateValidationJob : IJob
{
    private readonly ApplicationDbContext context;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpirationDateValidationJob"/> class.
    /// </summary>
    /// <param name="context">Application database context.</param>
    public ExpirationDateValidationJob(ApplicationDbContext context)
    {
        this.context = context;
    }

    /// <summary>
    /// Methods used to execute the background job.
    /// </summary>
    /// <param name="context">Job execution context.</param>
    /// <returns>A synchronous void task.</returns>
    public async Task Execute(IJobExecutionContext context)
    {
        var medicines = await this.context.Medicines.ToListAsync();

        foreach (var medicine in medicines)
        {
            if (medicine.ExpirationDate < DateOnly.FromDateTime(DateTime.UtcNow))
            {
                medicine.IsExpired = true;
            }
        }

        await this.context.SaveChangesAsync();
    }
}
