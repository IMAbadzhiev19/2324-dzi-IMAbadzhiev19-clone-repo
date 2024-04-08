using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PMS.Data.Models.Auth;
using PMS.Services.Contracts;
using PMS.Services.Contracts.Pharmacy;
using PMS.Services.Enums;
using PMS.Shared;
using PMS.Shared.Contracts;
using PMS.Shared.Models.Medicines;

namespace PMS.WebHost.Controllers;

/// <summary>
/// Controller for handling medicine-related operations.
/// </summary>
[ApiController]
[Route("api/medicine")]
//[EnableRateLimiting("fixed")]
public class MedicineController : ControllerBase
{
    private readonly IMedicineService medicineService;
    private readonly IEmailService emailService;
    private readonly ICurrentUser currentUser;
    private readonly ILogger<MedicineController> logger;
    private readonly IActivityService activityService;

    /// <summary>
    /// Initializes a new instance of the <see cref="MedicineController"/> class.
    /// </summary>
    /// <param name="medicineService">The medicine service.</param>
    /// <param name="emailService">The email service.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="currentUser">The current user service.</param>
    /// <param name="activityService">The activity service.</param>
    public MedicineController(IMedicineService medicineService, IEmailService emailService, ILogger<MedicineController> logger, ICurrentUser currentUser, IActivityService activityService)
    {
        this.medicineService = medicineService;
        this.emailService = emailService;
        this.logger = logger;
        this.currentUser = currentUser;
        this.activityService = activityService;
    }

    /// <summary>
    /// Creates a new medicine.
    /// </summary>
    /// <param name="medicineIM">The medicine input model.</param>
    /// <param name="buildingId">The id of the building.</param>
    /// <param name="buildingType">The type of the building..</param>
    /// <returns>The result of the medicine creation operation.</returns>
    [HttpPost("create")]
    [Authorize]
    public async Task<IActionResult> CreateMedicineAsync([FromBody] MedicineIM medicineIM, [FromQuery] string buildingId, [FromQuery] BuildingType buildingType)
    {
        try
        {
            this.logger.LogInformation($"User:{this.currentUser.UserId} is trying to create a medicine");
            var id = await this.medicineService.CreateMedicineAsync(medicineIM, buildingType, buildingId);
            await this.activityService.ChangeLastRequestAsync(this.currentUser.UserId);
            this.logger.LogInformation($"User:{this.currentUser.UserId} has successfully created a medicine");

            return this.Ok(id);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message);
            return this.BadRequest(new Response
            {
                Status = "create-medicine-failed",
                Message = ex.Message,
            });
        }
    }

    /// <summary>
    /// Updates an existing medicine.
    /// </summary>
    /// <param name="medicineUM">The medicine update model.</param>
    /// <returns>The result of the medicine update operation.</returns>
    [HttpPut("update")]
    [Authorize]
    public async Task<IActionResult> UpdateMedicineAsync([FromForm] MedicineUM medicineUM)
    {
        try
        {
            this.logger.LogInformation($"User: {this.currentUser.UserId} is trying to update a medicine with id: {medicineUM.Id}");
            await this.medicineService.UpdateMedicineAsync(medicineUM);
            await this.activityService.ChangeLastRequestAsync(this.currentUser.UserId);
            this.logger.LogInformation($"User: {this.currentUser.UserId} has successfully updated a medicine with id: {medicineUM.Id}");

            return this.Ok(new Response
            {
                Status = "update-medicine-success",
                Message = "Medicine updated successfully",
            });
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message);
            return this.BadRequest(new Response
            {
                Status = "update-medicine-failed",
                Message = ex.Message,
            });
        }
    }

    /// <summary>
    /// Deletes a medicine.
    /// </summary>
    /// <param name="id">The ID of the medicine to delete.</param>
    /// <returns>The result of the medicine deletion operation.</returns>
    [HttpDelete("delete/{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteMedicineAsync([FromRoute] string id)
    {
        try
        {
            this.logger.LogInformation($"User: {this.currentUser.UserId} is trying to delete a medicine with id: {id}");
            await this.medicineService.DeleteMedicineAsync(id);
            await this.activityService.ChangeLastRequestAsync(this.currentUser.UserId);
            this.logger.LogInformation($"User: {this.currentUser.UserId} has successfully deleted a medicine with id: {id}");

            return this.Ok(new Response
            {
                Status = "delete-medicine-success",
                Message = "Medicine deleted successfully",
            });
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message);
            return this.BadRequest(new Response
            {
                Status = "delete-medicine-failed",
                Message = ex.Message,
            });
        }
    }

    /// <summary>
    /// Retrieves a medicine by its ID.
    /// </summary>
    /// <param name="id">The ID of the medicine to retrieve.</param>
    /// <returns>The result of the medicine retrieval operation.</returns>
    [HttpGet("medicine/{id}")]
    [Authorize]
    public async Task<ActionResult<MedicineVM>> GetMedicineByIdAsync([FromRoute] string id)
    {
        try
        {
            this.logger.LogInformation($"User: {this.currentUser.UserId} is trying to get a medicine with id: {id}");
            var medicine = await this.medicineService.GetMedicineByIdAsync(id);
            await this.activityService.CreateActivityForUserAsync(this.currentUser.UserId);
            this.logger.LogInformation($"User: {this.currentUser.UserId} has successfully got a medicine with id: {id}");
            return this.Ok(medicine);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message);
            return this.BadRequest(new Response
            {
                Status = "get-medicine-failed",
                Message = ex.Message,
            });
        }
    }

    /// <summary>
    /// Retrieves all medicines.
    /// </summary>
    /// <param name="buildingId">The ID of the pharmacy from which we need the medicines.</param>
    /// <param name="type">The type of the building.</param>
    /// <returns>The result of the medicines retrieval operation.</returns>
    [HttpGet("medicines/{buildingId}")]
    [Authorize]
    public async Task<ActionResult<MedicineVM[]>> GetMedicinesAsync([FromRoute] string buildingId, [FromQuery] BuildingType type)
    {
        try
        {
            this.logger.LogInformation($"User: {this.currentUser.UserId} is trying to get all medicines");
            var medicines = await this.medicineService.GetAllMedicinesAsync(type, buildingId);
            this.logger.LogInformation($"User: {this.currentUser.UserId} has successfully got all medicines");
            await this.activityService.CreateActivityForUserAsync(this.currentUser.UserId);
            return this.Ok(medicines);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message);
            return this.BadRequest(new Response
            {
                Status = "get-medicines-failed",
                Message = "Medicines retrieval failed",
            });
        }
    }

    /// <summary>
    /// Requests a new basic medicine.
    /// </summary>
    /// <param name="basicMedicineRequest">The basic medicine request.</param>
    /// <returns>The result of the basic medicine request operation.</returns>
    [HttpPost("request-basic-medicine")]
    [Authorize(Roles = $"{UserRoles.Pharmacist},{UserRoles.Founder}")]
    public async Task<IActionResult> RequestBasicMedicineAsync([FromBody] BasicMedicineRequest basicMedicineRequest)
    {
        try
        {
            this.logger.LogInformation($"User:{this.currentUser.UserId} is trying to request a new basic medicine");
            var emailRequest = new IEmailService.SendEmailRequest(
                basicMedicineRequest.Email,
                "New basic medicine request",
                $"Please add this basic medicine: {basicMedicineRequest.Name}",
                null,
                null);

            await this.emailService.SendEmailAsync(emailRequest);
            this.logger.LogInformation($"User:{this.currentUser.UserId} has successfully requested a new basic medicine");

            return this.Ok(new Response
            {
                Status = "request-success",
                Message = "Basic medicine successfully requested",
            });
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message);
            return this.BadRequest(new Response
            {
                Status = "request-failed",
                Message = "Basic medicine request failed",
            });
        }
    }

    /// <summary>
    /// Retrieves all basic medicines.
    /// </summary>
    /// <returns>All basic medicines from the database.</returns>
    [HttpGet("basic-medicines")]
    [Authorize]
    public async Task<ActionResult<BasicMedicineVM[]>> GetBasicMedicinesAsync()
    {
        try
        {
            this.logger.LogInformation($"User: {this.currentUser.UserId} is trying to get all medicines");
            var basicMedicines = await this.medicineService.GetBasicMedicinesAsync();
            this.logger.LogInformation($"User: {this.currentUser.UserId} has successfully got all medicines");

            return this.Ok(basicMedicines);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message);
            return this.BadRequest(new Response
            {
                Status = "get-basic-medicines-failed",
                Message = "BasicMedicines retrieval failed",
            });
        }
    }

    /// <summary>
    /// Refills a medicine with given id.
    /// </summary>
    /// <param name="id">The ID of the medicine.</param>
    /// <param name="refillIM">The refill input model.</param>
    /// <returns>A result containing the result of the opration.</returns>
    [HttpPost("refill/{id}")]
    [Authorize]
    public async Task<IActionResult> RefillMedicinesAsync([FromRoute] string id, [FromBody] RefillIM refillIM)
    {
        try
        {
            await this.medicineService.RefillQuantityAsync(refillIM.Quantity, refillIM.PharmacyId, id);
            await this.activityService.ChangeLastRequestAsync(this.currentUser.UserId);

            return this.Ok(new Response
            {
                Status = "refill-success",
                Message = "Successfully refilled medicine",
            });
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error occured");
            return this.BadRequest(new Response
            {
                Status = "refill-failed",
                Message = ex.Message,
            });
        }
    }
}