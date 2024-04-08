using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using PMS.Services.Contracts.Auth;
using PMS.Services.Contracts.Pharmacy;
using PMS.Shared;
using PMS.Shared.Contracts;
using PMS.Shared.Models.User.Auth;
using PMS.Shared.Models.User.Auth.Token;

namespace PMS.WebHost.Controllers;

/// <summary>
/// Controller for handling authentication-related operations.
/// </summary>
[ApiController]
[Route("api/auth")]
//[EnableRateLimiting("fixed")]
public class AuthController : ControllerBase
{
    private readonly IAuthService authService;
    private readonly IUserService userService;
    private readonly ITokenService tokenService;
    private readonly ICurrentUser currentUser;
    private readonly IActivityService activityService;
    private readonly ILogger<AuthController> logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthController"/> class.
    /// </summary>
    /// <param name="authService">The authentication service.</param>
    /// <param name="tokenService">The token service.</param>
    /// <param name="userService">The user service.</param>
    /// <param name="currentUser">The current user service.</param>
    /// <param name="logger">The logger.</param>
    /// <param name="activityService">The activity service.</param>
    public AuthController(IAuthService authService, ITokenService tokenService, IUserService userService, ICurrentUser currentUser, ILogger<AuthController> logger, IActivityService activityService)
    {
        this.authService = authService;
        this.tokenService = tokenService;
        this.currentUser = currentUser;
        this.userService = userService;
        this.logger = logger;
        this.activityService = activityService;
    }

    /// <summary>
    /// Registers a new user.
    /// </summary>
    /// <param name="registerIM">The registration input model.</param>
    /// <returns>The result of the registration operation.</returns>
    [HttpPost("register")]
    public async Task<IResult> RegisterAsync([FromBody] RegisterIM registerIM)
    {
        this.logger.LogInformation($"User with email: {registerIM.Email} is trying to register.");
        if (await this.authService.CheckIfUserExistsAsync(registerIM.Email))
        {
            this.logger.LogWarning($"User with email: {registerIM.Email} already exists.");
            return Results.Conflict(new Response
            {
                Status = "register-failed",
                Message = "User with email already exists",
            });
        }

        try
        {
            var usedId = await this.authService.CreateUserAsync(registerIM);
            this.logger.LogInformation($"User with email: {registerIM.Email} has successfully registered.");

            return Results.Ok(usedId);
        }
        catch (Exception ex)
        {
            this.logger.LogError(ex.Message);

            return Results.BadRequest(new Response
            {
                    Status = "register-failed",
                    Message = ex.Message,
            });
        }
    }

    /// <summary>
    /// Logs a user in.
    /// </summary>
    /// <param name="loginIM">The login input model.</param>
    /// <returns>The result of the login operation.</returns>
    [HttpPost("login")]
    public async Task<IResult> LoginAsync([FromBody] LoginIM loginIM)
    {
        this.logger.LogInformation($"User with email {loginIM.Email} is trying to login.");
        if (!await this.authService.CheckIfUserExistsAsync(loginIM.Email))
        {
            this.logger.LogWarning($"User with email: {loginIM.Email} does not exist");
            return Results.BadRequest(new Response
            {
                Status = "login-failed",
                Message = "Invalid email",
            });
        }

        if (!await this.authService.CheckPasswordAsync(loginIM.Email, loginIM.Password))
        {
            this.logger.LogWarning($"User with email: {loginIM.Email} entered invalid password");
            return Results.BadRequest(new Response
            {
                Status = "login-failed",
                Message = "Invalid password",
            });
        }

        var tokens = await this.tokenService.CreateTokenForUserAsync(loginIM.Email);
        this.logger.LogInformation($"User with email: {loginIM.Email} has successfully logged in.");

        return Results.Ok(new
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(tokens.AccessToken),
            RefreshToken = new JwtSecurityTokenHandler().WriteToken(tokens.RefreshToken),
            Expiration = tokens.AccessToken!.ValidTo,
        });
    }

    /// <summary>
    /// Logs a user out.
    /// </summary>
    /// <returns>The result of the logout operation.</returns>
    [HttpGet("logout")]
    [Authorize]
    public async Task<IResult> LogoutAsync()
    {
        var email = (await this.userService.GetUserByIdAsync(this.currentUser.UserId)).Email;
        this.logger.LogInformation($"User with email {email} is trying to logout.");

        await this.tokenService.DeleteRefreshTokenAsync(this.currentUser.UserId);
        this.logger.LogInformation($"User with email {email} has successfully logged out.");

        return Results.Ok(new Response
        {
            Status = "logout-success",
            Message = "User has successfully logged out",
        });
    }

    /// <summary>
    /// Renews access and refresh tokens.
    /// </summary>
    /// <param name="tokensIM">The tokens input model.</param>
    /// <returns>The result of the token renewal operation.</returns>
    [HttpPost("renew-tokens")]
    public async Task<IResult> RefreshTokensAsync([FromBody] TokensIM tokensIM)
    {
        if (tokensIM is null)
        {
            return Results.BadRequest(
                new Response
                {
                    Status = "renew-tokens-failed",
                    Message = "No tokens provided.",
                });
        }

        var tokens = await this.tokenService.CreateNewTokensAsync(tokensIM);
        if (tokens.AccessToken is null)
        {
            return Results.BadRequest(new Response
            {
                Status = "renew-tokens-failed",
                Message = "Invalid token.",
            });
        }

        return Results.Ok(new
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(tokens.AccessToken),
            RefreshToken = new JwtSecurityTokenHandler().WriteToken(tokens.RefreshToken),
            Expiration = tokens.AccessToken!.ValidTo,
        });
    }
}