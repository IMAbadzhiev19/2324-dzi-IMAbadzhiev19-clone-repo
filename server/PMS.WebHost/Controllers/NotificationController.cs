using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using PMS.Services.Contracts.Pharmacy;
using PMS.Services.Enums;
using PMS.Shared;
using PMS.Shared.Models;
using PMS.Shared.Models.Notifications;

namespace PMS.WebHost.Controllers;

/// <summary>
/// Controller for handling notification-related operations.
/// </summary>
[ApiController]
[Route("api/notification")]
//[EnableRateLimiting("fixed")]
public class NotificationController : ControllerBase
{
    private readonly INotificationService notificationService;
    private readonly ILogger<NotificationController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="NotificationController"/> class.
    /// </summary>
    /// <param name="notificationService">The notification service.</param>
    /// <param name="logger">The logger.</param>
    public NotificationController(INotificationService notificationService, ILogger<NotificationController> logger)
    {
        this.notificationService = notificationService;
        this.logger = logger;
    }

    /// <summary>
    /// Retrieves all notifications.
    /// </summary>
    /// <param name="pharmacyId">The ID of the pharmacy.</param>
    /// <param name="depotId">The ID of the depot.</param>
    /// <returns>The result of the notification retrieval operation.</returns>
    [Authorize]
    [HttpGet("get")]
    public async Task<ActionResult<NotificationVM[]>> GetNotificationsAsync([FromQuery] string pharmacyId, [FromQuery] string depotId)
    {
        try
        {
            var notifications = await this.notificationService.GetNotificationsAsync(pharmacyId, depotId);

            return this.Ok(notifications);
        }
        catch (Exception ex)
        {
            return this.BadRequest(new Response
            {
                Status = "get-notifications-failed",
                Message = ex.Message,
            });
        }
    }

    /// <summary>
    /// Retrieves all notifications.
    /// </summary>
    /// <param name="buildingId">The ID of the building.</param>
    /// <param name="buildingType">The type of the building.</param>
    /// <returns>The result of the notification retrieval operation.</returns>
    [Authorize]
    [HttpGet("warnings")]
    public async Task<ActionResult<NotificationVM[]>> GetWarningNotificationsAsync([FromQuery] string buildingId, [FromQuery] BuildingType buildingType)
    {
        try
        {
            var notifications = await this.notificationService.GetWarningsAsync(buildingId, buildingType);

            return this.Ok(notifications);
        }
        catch (Exception ex)
        {
            return this.BadRequest(new Response
            {
                Status = "get-notifications-failed",
                Message = ex.Message,
            });
        }
    }

    /// <summary>
    /// Retrieves all notifications representing assign requests.
    /// </summary>
    /// <param name="depotId">The ID of the depot.</param>
    /// <returns>The result of the notification retrieval operation.</returns>
    [Authorize]
    [HttpGet("assign-requests")]
    public async Task<ActionResult<NotificationVM[]>> GetAssignRequestsAsync([FromQuery] string depotId)
    {
        try
        {
            var notifications = await this.notificationService.GetAssignRequestsAsync(depotId);

            return this.Ok(notifications);
        }
        catch (Exception ex)
        {
            return this.BadRequest(new Response
            {
                Status = "get-notifications-failed",
                Message = ex.Message,
            });
        }
    }

    /// <summary>
    /// Retrieves all notifications for pharmacy.
    /// </summary>
    /// <param name="pharmacyId">The ID of the pharmacy.</param>
    /// <returns>The result of the notification retrieval operation.</returns>
    [Authorize]
    [HttpGet("get-pharmacies/{pharmacyId}")]
    public async Task<ActionResult<NotificationVM[]>> GetNotificationsForPharmacyAsync([FromRoute] string pharmacyId)
    {
        try
        {
            var notifications = await this.notificationService.GetNotificationsForPharmacyAsync(pharmacyId);

            return this.Ok(notifications);
        }
        catch (Exception ex)
        {
            return this.BadRequest(new Response
            {
                Status = "get-notifications-failed",
                Message = ex.Message,
            });
        }
    }

    /// <summary>
    /// Retrieves all notifications for depot.
    /// </summary>
    /// <param name="depotId">The ID of the depot.</param>
    /// <returns>The result of the notification retrieval operation.</returns>
    [Authorize]
    [HttpGet("get-depots/{depotId}")]
    public async Task<ActionResult<NotificationVM[]>> GetNotificationsForDepotAsync([FromRoute] string depotId)
    {
        try
        {
            var notifications = await this.notificationService.GetNotificationsForDepotAsync(depotId);

            return this.Ok(notifications);
        }
        catch (Exception ex)
        {
            return this.BadRequest(new Response
            {
                Status = "get-notifications-failed",
                Message = ex.Message,
            });
        }
    }
}