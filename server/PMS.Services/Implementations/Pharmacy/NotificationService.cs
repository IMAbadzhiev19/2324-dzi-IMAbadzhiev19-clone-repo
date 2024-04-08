using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using PMS.Data;
using PMS.Services.Contracts.Pharmacy;
using PMS.Services.Enums;
using PMS.Shared.Models.Notifications;

namespace PMS.Services.Implementations.Pharmacy;

/// <summary>
/// A class responsible for notification-related operations.
/// </summary>
internal class NotificationService : INotificationService
{
    private readonly ApplicationDbContext context;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationService"/> class.
    /// </summary>
    /// <param name="context">Application database context.</param>
    /// <param name="mapper">Auto mapper.</param>
    public NotificationService(ApplicationDbContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<ICollection<NotificationVM>> GetAssignRequestsAsync(string depotId)
    {
        return await this.context.Notifications
            .Where(n => n.IsAssignRequest && n.DepotId == depotId)
            .Include(n => n.Pharmacy)
            .Include(n => n.Depot)
            .ProjectTo<NotificationVM>(this.mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<ICollection<NotificationVM>> GetNotificationsAsync(string pharmacyId, string depotId)
    {
        return await this.context.Notifications
            .Where(n => n.PharmacyId == pharmacyId && n.DepotId == depotId)
            .Include(n => n.Pharmacy)
            .Include(n => n.Depot)
            .ProjectTo<NotificationVM>(this.mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<ICollection<NotificationVM>> GetNotificationsForDepotAsync(string depotId)
    {
        return await this.context.Notifications
            .Where(n => n.DepotId == depotId && (n.PharmacyId == null))
            .Include(n => n.Pharmacy)
            .Include(n => n.Depot)
            .ProjectTo<NotificationVM>(this.mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<ICollection<NotificationVM>> GetNotificationsForPharmacyAsync(string pharmacyId)
    {
        return await this.context.Notifications
            .Where(n => n.PharmacyId == pharmacyId && (n.DepotId == null))
            .Include(n => n.Pharmacy)
            .Include(n => n.Depot)
            .ProjectTo<NotificationVM>(this.mapper.ConfigurationProvider)
            .AsNoTracking()
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<ICollection<NotificationVM>> GetWarningsAsync(string buildingId, BuildingType buildingType)
    {
        if (buildingType is BuildingType.Pharmacy)
        {
            return await this.context.Notifications
                .Where(n => n.PharmacyId == buildingId && n.IsWarning == true)
                .Include(n => n.Pharmacy)
                .ProjectTo<NotificationVM>(this.mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();
        }
        else
        {
            return await this.context.Notifications
                .Where(n => n.DepotId == buildingId && n.IsWarning == true)
                .Include(n => n.Depot)
                .ProjectTo<NotificationVM>(this.mapper.ConfigurationProvider)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}