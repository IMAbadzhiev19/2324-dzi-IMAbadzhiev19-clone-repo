using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using PMS.Data.Models.Auth;
using PMS.Services.Contracts.Auth;
using PMS.Services.Contracts.Pharmacy;
using PMS.Shared;
using PMS.Shared.Contracts;
using PMS.Shared.Models.Depots;

namespace PMS.WebHost.Controllers;

/// <summary>
/// Controller for handling depot-related operations.
/// </summary>
[ApiController]
[Route("api/depot")]
//[EnableRateLimiting("fixed")]
public class DepotController : ControllerBase
{
    private readonly IDepotService depotService;
    private readonly ICurrentUser currentUser;
    private readonly ILogger<DepotController> logger;
    private readonly IAuthService authService;

    /// <summary>
    /// Initializes a new instance of the <see cref="DepotController"/> class.
    /// </summary>
    /// <param name="depotService">The depot service.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="currentUser">The current user service.</param>
    /// <param name="authService">The auth service.</param>
    public DepotController(IDepotService depotService, ILogger<DepotController> logger, ICurrentUser currentUser, IAuthService authService)
    {
        this.depotService = depotService;
        this.logger = logger;
        this.currentUser = currentUser;
        this.authService = authService;
    }

    /// <summary>
    /// Creates a new depot.
    /// </summary>
    /// <param name="depotIM">The depot input model.</param>
    /// <returns>The ID of the newly created depot.</returns>
    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> CreateDepotAsync([FromBody] DepotIM depotIM)
    {
        try
        {
            this.logger.LogInformation($"User: {this.currentUser.UserId} is trying to create a depot");
            var id = await this.depotService.CreateDepotAsync(depotIM, this.currentUser.UserId);
            this.logger.LogInformation($"User: {this.currentUser.UserId} has successfully created a depot");
            await this.authService.AddRoleToUserAsync(this.currentUser.UserId, UserRoles.DepotManager);
            this.logger.LogInformation($"User: {this.currentUser.UserId} has successfully been assigned the role of DepotManager");

            return this.Ok(id);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message);
            return this.BadRequest(new Response
            {
                Status = "create-failed",
                Message = "Create depot failed",
            });
        }
    }

    /// <summary>
    /// Updates an existing depot.
    /// </summary>
    /// <param name="depotUM">The updated depot model.</param>
    /// <returns>A response indicating the status of the update operation.</returns>
    [HttpPut("update")]
    [Authorize]
    public async Task<IActionResult> UpdateDepotAsync([FromBody] DepotUM depotUM)
    {
        try
        {
            this.logger.LogInformation($"User: {this.currentUser.UserId} is trying to update depot with id: {depotUM.Id}");
            await this.depotService.UpdateDepotAsync(depotUM);
            this.logger.LogInformation($"User: {this.currentUser.UserId} has successfully updated a depot with id: {depotUM.Id}");

            return this.Ok(new Response
            {
                Status = "update-success",
                Message = "Update depot succeeded",
            });
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message);
            return this.BadRequest(new Response
            {
                Status = "update-failed",
                Message = "Update depot failed",
            });
        }
    }

    /// <summary>
    /// Deletes a depot.
    /// </summary>
    /// <param name="id">The ID of the depot to delete.</param>
    /// <returns>A response indicating the status of the delete operation.</returns>
    [HttpDelete("delete/{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteDepotAsync([FromRoute] string id)
    {
        try
        {
            this.logger.LogInformation($"User: {this.currentUser.UserId} is trying to delete depot with id: {id}");
            await this.depotService.DeleteDepotAsync(id);
            this.logger.LogInformation($"User: {this.currentUser.UserId} has successfully deleted a depot with id: {id}");

            return this.Ok(new Response
            {
                Status = "delete-success",
                Message = "Delete depot succeeded",
            });
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message);
            return this.BadRequest(new Response
            {
                Status = "delete-failed",
                Message = "Delete depot failed",
            });
        }
    }

    /// <summary>
    /// Retrieves a depot by its ID.
    /// </summary>
    /// <param name="id">The ID of the depot to retrieve.</param>
    /// <returns>The depot with the specified ID.</returns>
    [HttpGet("depot/{id}")]
    [Authorize]
    public async Task<ActionResult<DepotVM>> GetDepotByIdAsync([FromRoute] string id)
    {
        try
        {
            this.logger.LogInformation($"User: {this.currentUser.UserId} is trying to get depot with id: {id}");
            var depot = await this.depotService.GetDepotByIdAsync(id);
            this.logger.LogInformation($"User: {this.currentUser.UserId} has successfully retrieved a depot with id: {id}");
            return this.Ok(depot);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message);
            return this.BadRequest(new Response
            {
                Status = "get-failed",
                Message = "Get depot failed",
            });
        }
    }

    /// <summary>
    /// Retrieves all depots.
    /// </summary>
    /// <returns>A list of all depots.</returns>
    [HttpGet("depots")]
    [Authorize]
    public async Task<ActionResult<DepotVM[]>> GetAllDepotsAsync()
    {
        try
        {
            this.logger.LogInformation($"User: {this.currentUser.UserId} is trying to get all depots");
            var depots = await this.depotService.GetAllDepotsAsync();
            this.logger.LogInformation($"User: {this.currentUser.UserId} has successfully retrieved all depots");
            return this.Ok(depots);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message);
            return this.BadRequest(new Response
            {
                Status = "get-all-failed",
                Message = "Get all depots failed",
            });
        }
    }

    /// <summary>
    /// Retrieves all depots owned by a specific user.
    /// </summary>
    /// <returns></returns>
    [HttpGet("depots-by-user")]
    [Authorize]
    public async Task<ActionResult<DepotVM[]>> GetDepotsByUserIdAsync()
    {
        try
        {
            this.logger.LogInformation($"User: {this.currentUser.UserId} is trying to get all owned depots");
            var depots = await this.depotService.GetDepotsByUserIdAsync(this.currentUser.UserId);
            this.logger.LogInformation($"User: {this.currentUser.UserId} has successfully retrieved all owned depots");
            return this.Ok(depots);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message);
            return this.BadRequest(new Response
            {
                Status = "get-all-failed",
                Message = "Get all depots owned by user failed",
            });
        }
    }

    /// <summary>
    /// Assigns a depot to a pharmacy.
    /// </summary>
    /// <param name="pharmacyId">The ID of the pharmacy.</param>
    /// <param name="depotId">The ID of the depot.</param>
    /// <returns>A response indicating the status of the assignment operation.</returns>
    [HttpPost("assign-to-pharmacy")]
    [Authorize]
    public async Task<IActionResult> AssignDepotToPharmacyAsync([FromQuery] string pharmacyId, [FromQuery] string depotId)
    {
        try
        {
            this.logger.LogInformation($"User: {this.currentUser.UserId} is trying to assign depot with id: {depotId} to pharmacy with id: {pharmacyId}");
            await this.depotService.AssignDepotToPharmacyAsync(depotId, pharmacyId);
            this.logger.LogInformation($"User: {this.currentUser.UserId} has successfully assigned depot with id: {depotId} to pharmacy with id: {pharmacyId}");
            return this.Ok(new Response
            {
                Status = "assign-to-pharmacy-success",
                Message = "Assign depot succeeded",
            });
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message);
            return this.BadRequest(new Response
            {
                Status = "assign-to-pharmacy-failed",
                Message = "Assign depot failed",
            });
        }
    }
}