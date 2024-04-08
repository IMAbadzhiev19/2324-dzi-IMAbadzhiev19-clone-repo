using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using PMS.Services.Contracts.Auth;
using PMS.Shared;
using PMS.Shared.Contracts;
using PMS.Shared.Models;
using PMS.Shared.Models.User;

namespace PMS.WebHost.Controllers;

/// <summary>
/// Controller for handling user-related operations.
/// </summary>
[ApiController]
[Route("api/user")]
//[EnableRateLimiting("fixed")]
public class UserController : ControllerBase
{
    private readonly IUserService userService;
    private readonly ICurrentUser currentUser;
    private readonly ILogger<UserController> logger;
    private readonly IAuthService authService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserController"/> class.
    /// </summary>
    /// <param name="userService">The user service.</param>
    /// <param name="currentUser">The current user.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="authService">The auth service.</param>
    public UserController(IUserService userService, ICurrentUser currentUser, ILogger<UserController> logger, IAuthService authService)
    {
        this.userService = userService;
        this.currentUser = currentUser;
        this.logger = logger;
        this.authService = authService;
    }

    /// <summary>
    /// Retrieves the current user's info.
    /// </summary>
    /// <returns>The result of the operation.</returns>
    [HttpGet("current-user")]
    [Authorize]
    public async Task<ActionResult<UserVM>> GetUserAsync()
    {
        try
        {
            var user = await this.userService.GetUserByIdAsync(this.currentUser.UserId);
            return this.Ok(user);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, "Error occured during fetching user's info");
            return this.BadRequest(new Response
            {
                Status = "current-user-failed",
                Message = ex.Message,
            });
        }
    }

    /// <summary>
    /// Updates the information of the current user.
    /// </summary>
    /// <param name="userUM">The updated user information.</param>
    /// <returns>The result of the user update operation.</returns>
    [HttpPut("update")]
    [Authorize]
    public async Task<IActionResult> UpdateUserAsync([FromBody] UserUM userUM)
    {
        try
        {
            this.logger.LogInformation($"User with id: {this.currentUser.UserId} is trying to update his/her info.");
            await this.userService.UpdateUserInfoAsync(this.currentUser.UserId, userUM);
            this.logger.LogInformation($"User with id: {this.currentUser.UserId} has successfully updated his/her info.");

            return this.Ok(new Response
            {
                Status = "update-failed",
                Message = "Update user failed",
            });
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, $"Updating information for user with id: {this.currentUser.UserId} failed.");
            return this.BadRequest(new Response
            {
                Status = "update-failed",
                Message = ex.Message,
            });
        }
    }

    /// <summary>
    /// Changes the user's password.
    /// </summary>
    /// <param name="changePasswordIM">The input model for changing password.</param>
    /// <returns>The result of the password change operation.</returns>
    [HttpPut("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePasswordAsync([FromBody] ChangePasswordIM changePasswordIM)
    {
        try
        {
            this.logger.LogInformation($"User with id: {this.currentUser.UserId} is trying to update his/her password.");
            var verified = await this.authService.CheckPasswordAsync(
                (await this.userService.GetUserByIdAsync(this.currentUser.UserId)).Email,
                changePasswordIM.OldPassword);

            if (!verified)
            {
                throw new ArgumentException("Old password does not exist");
            }

            var result = await this.userService.ChangePasswordAsync(this.currentUser.UserId, changePasswordIM.NewPassword);
            this.logger.LogInformation($"User with id: {this.currentUser.UserId} has successfully updated his/her password.");

            return this.Ok(new Response
            {
                Status = "change-password-success",
                Message = "Successfully changed password",
            });
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex, $"Failed to change password for user with id: {this.currentUser.UserId}.");

            return this.BadRequest(new Response
            {
                Status = "change-password-failed",
                Message = ex.Message,
            });
        }
    }
}