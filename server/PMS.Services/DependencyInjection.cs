using Microsoft.Extensions.DependencyInjection;
using PMS.Services.Contracts;
using PMS.Services.Contracts.Auth;
using PMS.Services.Contracts.Pharmacy;
using PMS.Services.Implementations;
using PMS.Services.Implementations.Auth;
using PMS.Services.Implementations.BackgroundJobs;
using PMS.Services.Implementations.Pharmacy;
using PMS.Services.Mapping;
using PMS.Shared.Contracts;
using PMS.Shared.Options;
using Quartz;

namespace PMS.Services;

/// <summary>
/// A statis class used for injecting all of the services.
/// </summary>
public static class DependencyInjection
{
    /// <summary>
    /// The method responsible for injecting the services.
    /// </summary>
    /// <param name="services">The service collection.</param>
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<ICurrentUser, CurrentUser>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IDepotService, DepotService>();
        services.AddScoped<IMedicineService, MedicineService>();
        services.AddScoped<IInvoiceService, InvoiceService>();
        services.AddScoped<IPharmacyService, PharmacyService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IActivityService, ActivityService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<IFileService, FileService>();

        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        services.AddQuartz(options =>
        {
            options.UseMicrosoftDependencyInjectionJobFactory();

            var medicineValidationJobKey = JobKey.Create(nameof(ExpirationDateValidationJob));
            options
                .AddJob<ExpirationDateValidationJob>(medicineValidationJobKey)
                .AddTrigger(trigger =>
                    trigger
                        .ForJob(medicineValidationJobKey)
                        .WithCronSchedule(CronScheduleBuilder.DailyAtHourAndMinute(9, 00)));

            var workingHoursJobKey = JobKey.Create(nameof(WorkingHoursValidation));

            options
                .AddJob<WorkingHoursValidation>(workingHoursJobKey)
                .AddTrigger(trigger =>
                    trigger
                        .ForJob(workingHoursJobKey)
                        .WithCronSchedule(CronScheduleBuilder.WeeklyOnDayAndHourAndMinute(DayOfWeek.Monday, 00, 00)));
        });

        services.AddQuartzHostedService();

        services.AddHttpContextAccessor();
    }
}