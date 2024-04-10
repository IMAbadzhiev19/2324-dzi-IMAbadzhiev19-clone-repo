using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using PMS.Data.Models.Auth;
using PMS.Services.Contracts;
using PMS.Services.Contracts.Auth;
using PMS.Services.Contracts.Pharmacy;
using PMS.Shared;
using PMS.Shared.Contracts;
using PMS.Shared.Models.Medicines;
using PMS.Shared.Models.Pharmacies;
using PMS.Shared.Models.User;

namespace PMS.WebHost.Controllers;

/// <summary>
/// Controller for handling pharmacy-related operations.
/// </summary>
[ApiController]
[Route("api/pharmacy")]
//[EnableRateLimiting("fixed")]
public class PharmacyController : ControllerBase
{
    private readonly IPharmacyService pharmacyService;
    private readonly ICurrentUser currentUser;
    private readonly IAuthService authService;
    private readonly IEmailService emailService;
    private readonly IUserService userService;
    private readonly ILogger<PharmacyController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="PharmacyController"/> class.
    /// </summary>
    /// <param name="pharmacyService">The pharmacy service.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="authService">The authentication service.</param>
    /// <param name="currentUser">The current user service.</param>
    /// <param name="emailService">The email service.</param>
    /// <param name="userService">The user service.</param>
    public PharmacyController(IPharmacyService pharmacyService, ILogger<PharmacyController> logger, IAuthService authService, ICurrentUser currentUser, IEmailService emailService, IUserService userService)
    {
        this.pharmacyService = pharmacyService;
        this.logger = logger;
        this.authService = authService;
        this.currentUser = currentUser;
        this.emailService = emailService;
        this.userService = userService;
    }

    /// <summary>
    /// Creates a new pharmacy.
    /// </summary>
    /// <param name="pharmacyIM">The input model for creating a pharmacy.</param>
    /// <returns>The result of the pharmacy creation operation.</returns>
    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> CreatePharmacyAsync([FromBody] PharmacyIM pharmacyIM)
    {
        try
        {
            this.logger.LogInformation($"User: {this.currentUser.UserId} is trying to create a pharmacy");
            var id = await this.pharmacyService.CreatePharmacyAsync(pharmacyIM, this.currentUser.UserId);
            this.logger.LogInformation($"User: {this.currentUser.UserId} has successfully created a pharmacy");
            await this.authService.AddRoleToUserAsync(this.currentUser.UserId, UserRoles.Founder);
            this.logger.LogInformation($"User: {this.currentUser.UserId} has successfully been assigned the role of Founder");

            return this.Ok(id);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message);
            return this.BadRequest(new Response
            {
                Status = "pharmacy-create-failed",
                Message = "Failed to create a pharmacy",
            });
        }
    }

    /// <summary>
    /// Updates an existing pharmacy.
    /// </summary>
    /// <param name="pharmacyUM">The update model for the pharmacy.</param>
    /// <returns>The result of the pharmacy update operation.</returns>
    [HttpPut("update")]
    [Authorize(Roles = UserRoles.Founder)]
    public async Task<IActionResult> UpdatePharmacyAsync([FromBody] PharmacyUM pharmacyUM)
    {
        try
        {
            this.logger.LogInformation($"User: {this.currentUser.UserId} is trying to update a pharmacy with id: {pharmacyUM.Id}");
            await this.pharmacyService.UpdatePharmacyAsync(pharmacyUM);
            this.logger.LogInformation($"User: {this.currentUser.UserId} has successfully updated a pharmacy with id: {pharmacyUM.Id}");

            return this.Ok(new Response
            {
                Status = "pharmacy-update-success",
                Message = "Successfully updated pharmacy",
            });
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message);
            return this.BadRequest(new Response
            {
                Status = "pharmacy-update-failed",
                Message = "Failed to update pharmacy",
            });
        }
    }

    /// <summary>
    /// Deletes a pharmacy by its ID.
    /// </summary>
    /// <param name="id">The ID of the pharmacy to delete.</param>
    /// <returns>The result of the pharmacy deletion operation.</returns>
    [HttpDelete("delete/{id}")]
    [Authorize(Roles = UserRoles.Founder)]
    public async Task<IActionResult> DeletePharmacyAsync([FromRoute] string id)
    {
        try
        {
            this.logger.LogInformation($"User: {this.currentUser.UserId} is trying to delete a pharmacy with id: {id}");
            await this.pharmacyService.DeletePharmacyAsync(id);

            var result = await this.pharmacyService.CheckIfUserHasAnyPharmacies(this.currentUser.UserId);
            if (!result)
            {
                await this.authService.RemoveRoleFromUserAsync(this.currentUser.UserId, UserRoles.Founder);
            }

            this.logger.LogInformation($"User: {this.currentUser.UserId} has successfully deleted a pharmacy with id: {id}");

            return this.Ok(new Response
            {
                Status = "pharmacy-delete-success",
                Message = "Successfully deleted pharmacy",
            });
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message);
            return this.BadRequest(new Response
            {
                Status = "pharmacy-delete-failed",
                Message = "Failed to delete pharmacy",
            });
        }
    }

    /// <summary>
    /// Retrieves a pharmacy by its ID.
    /// </summary>
    /// <param name="id">The ID of the pharmacy to retrieve.</param>
    /// <returns>The retrieved pharmacy.</returns>
    [HttpGet("pharmacy/{id}")]
    [Authorize]
    public async Task<ActionResult<PharmacyVM>> GetPharmacyByIdAsync([FromRoute] string id)
    {
        try
        {
            this.logger.LogInformation($"User: {this.currentUser.UserId} is trying to get a pharmacy with id: {id}");
            var pharmacy = await this.pharmacyService.GetPharmacyByIdAsync(id);
            this.logger.LogInformation($"User: {this.currentUser.UserId} has successfully got a pharmacy with id: {id}");
            return this.Ok(pharmacy);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message);
            return this.BadRequest(new Response
            {
                Status = "get-pharmacy-failed",
                Message = "Pharmacy retrieval failed",
            });
        }
    }

    /// <summary>
    /// Retrieves all pharmacies owned by user.
    /// </summary>
    /// <returns>All pharmacies.</returns>
    [HttpGet("pharmacies")]
    [Authorize]
    public async Task<ActionResult<PharmacyVM[]>> GetPharmaciesAsync()
    {
        try
        {
            this.logger.LogInformation($"User: {this.currentUser.UserId} is trying to get all pharmacies");
            var pharmacies = await this.pharmacyService.GetPharmaciesAsync(this.currentUser.UserId);
            this.logger.LogInformation($"User: {this.currentUser.UserId} has successfully got all pharmacies");
            return this.Ok(pharmacies);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message, "An error occured during fetching pharmacies");
            return this.BadRequest(new Response
            {
                Status = "get-pharmacies-failed",
                Message = ex.Message,
            });
        }
    }

    /// <summary>
    /// Retrieves all pharmacies by depot ID.
    /// </summary>
    /// <param name="depotId">The ID of the depot.</param>
    /// <returns>All pharmacies linked to depot.</returns>
    [HttpGet("pharmacies/{depotId}")]
    [Authorize]
    public async Task<ActionResult<PharmacyVM[]>> GetPharmaciesByDepotIdAsync([FromRoute] string depotId)
    {
        try
        {
            this.logger.LogInformation($"User: {this.currentUser.UserId} is trying to get all pharmacies linked to depot with id: {depotId}");
            var pharmacies = await this.pharmacyService.GetPharmaiesByDepotId(depotId);
            this.logger.LogInformation($"User: {this.currentUser.UserId} has successfully got all pharmacies linked to depot with id: {depotId}");
            return this.Ok(pharmacies);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message, "An error occured during fetching pharmacies");
            return this.BadRequest(new Response
            {
                Status = "get-pharmacies-failed",
                Message = ex.Message,
            });
        }
    }

    /// <summary>
    /// Assigns employee to pharmacy.
    /// </summary>
    /// <param name="pharmacyId">The ID of the pharmacy.</param>
    /// <param name="assignEmployeeIM">The assign employee input model.</param>
    /// <returns>The asynchronous result of the operation.</returns>
    [HttpPost("assign-employee/{pharmacyId}")]
    [Authorize]
    public async Task<IActionResult> AssignEmployeeAsync([FromRoute] string pharmacyId, [FromBody] AssignEmployeeIM assignEmployeeIM)
    {
        try
        {
            await this.pharmacyService.AssignEmployeeAsync(pharmacyId, assignEmployeeIM.Email);

            var emailRequest = new IEmailService.SendEmailRequest(assignEmployeeIM.Email, "New pharmacist position", $"In order to sign in the management system, you must use your email and the password: {assignEmployeeIM.Password} \r\n We suggest you change the password!", null, null);

            await this.emailService.SendEmailAsync(emailRequest);

            return this.Ok(new Response
            {
                Status = "assign-employee-success",
                Message = "Successfully assigned employee",
            });
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error occured");
            return this.BadRequest(new Response
            {
                Status = "assign-employee-failed",
                Message = ex.Message,
            });
        }
    }

    /// <summary>
    /// Removes employee from pharmacy.
    /// </summary>
    /// <param name="pharmacyId">The ID of the pharmacy.</param>
    /// <param name="employeeId">The ID of the employee.</param>
    /// <returns>The asynchronous result of the operation.</returns>
    [HttpPost("remove-employee/{pharmacyId}")]
    [Authorize]
    public async Task<IActionResult> RemoveEmployeeAsync([FromRoute] string pharmacyId, [FromQuery] string employeeId)
    {
        try
        {
            await this.pharmacyService.RemoveEmployeeAsync(pharmacyId, employeeId);

            return this.Ok(new Response
            {
                Status = "remove-employee-success",
                Message = "Successfully removed employee",
            });
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error occured");
            return this.BadRequest(new Response
            {
                Status = "remove-employee-failed",
                Message = ex.Message,
            });
        }
    }

    /// <summary>
    /// Retrieves all pharmacists working in the pharmacy.
    /// </summary>
    /// <param name="pharmacyId">The ID of the pharmacy.</param>
    /// <returns>The asynchronous result of the operation.</returns>
    [HttpGet("pharmacists/{pharmacyId}")]
    [Authorize]
    public async Task<ActionResult<UserVM[]>> GetPharmacistsAsync([FromRoute] string pharmacyId)
    {
        try
        {
            var pharmacists = await this.pharmacyService.GetPharmacistsAsync(pharmacyId);

            return this.Ok(pharmacists);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error occured");
            return this.BadRequest(new Response
            {
                Status = "pharmacists-failed",
                Message = ex.Message,
            });
        }
    }

    /// <summary>
    /// Requests depot assignment.
    /// </summary>
    /// <param name="pharmacyId">The ID of the pharmacy.</param>
    /// <param name="depotId">The ID of the depot.</param>
    /// <returns></returns>
    [HttpPost("request-depot-assign")]
    [Authorize]
    public async Task<IActionResult> RequestDepotAssignAsync([FromQuery] string pharmacyId, [FromQuery] string depotId)
    {
        try
        {
            await this.pharmacyService.RequestDepotAssignAsync(pharmacyId, depotId);

            return this.Ok(new Response
            {
                Status = "request-depot-assign-success",
                Message = "Successfully requested depot assign",
            });
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error occured");
            return this.BadRequest(new Response
            {
                Status = "request-depot-assign-failed",
                Message = ex.Message,
            });
        }
    }
}