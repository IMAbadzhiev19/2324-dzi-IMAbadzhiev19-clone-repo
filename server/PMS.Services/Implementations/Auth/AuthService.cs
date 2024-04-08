using Essentials.Results;
using Microsoft.AspNetCore.Identity;
using PMS.Data.Models.Auth;
using PMS.Services.Contracts.Auth;
using PMS.Shared.Models.User.Auth;

namespace PMS.Services.Implementations.Auth;

/// <summary>
/// A class responsible for auth-related operations.
/// </summary>
internal class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly RoleManager<IdentityRole> roleManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="AuthService"/> class.
    /// </summary>
    /// <param name="userManager">User manager.</param>
    /// <param name="roleManager">Role manager.</param>
    public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        this.userManager = userManager;
        this.roleManager = roleManager;
    }

    /// <inheritdoc />
    public async Task<MutationResult> CreateUserAsync(RegisterIM registerIM)
    {
        if (!await this.roleManager.RoleExistsAsync(UserRoles.BaseUser))
        {
            await this.roleManager.CreateAsync(new IdentityRole(UserRoles.BaseUser));
        }

        var user = new ApplicationUser
        {
            FirstName = registerIM.FirstName,
            LastName = registerIM.LastName,
            Email = registerIM.Email,
            PhoneNumber = registerIM.PhoneNumber,
            UserName = registerIM.Email!.Split('@')[0],
        };

        if (user is null)
        {
            throw new NullReferenceException();
        }

        var result = await this.userManager.CreateAsync(user, registerIM.Password);
        if (!result.Succeeded)
        {
            throw new Exception("User creation failed");
        }

        var roleResult = await this.userManager.AddToRoleAsync(user, UserRoles.BaseUser);
        if (!roleResult.Succeeded)
        {
            throw new Exception("Adding role failed");
        }

        var createdUser = await this.userManager.FindByEmailAsync(user.Email!);
        return MutationResult.ResultFrom(createdUser!.Id, "User has been created");
    }

    /// <inheritdoc />
    public async Task AddRoleToUserAsync(string userId, string roleType)
    {
        if (!await this.roleManager.RoleExistsAsync(roleType))
        {
            await this.roleManager.CreateAsync(new IdentityRole(roleType));
        }

        var user = await this.userManager.FindByIdAsync(userId);
        if (user is null)
        {
            throw new ArgumentException("Invalid user id");
        }

        var userRoles = await this.userManager.GetRolesAsync(user);
        if (!userRoles.Contains(roleType))
        {
            var result = await this.userManager.AddToRoleAsync(user, roleType);
            if (!result.Succeeded)
            {
                throw new Exception($"Adding user to \"{roleType}\" role failed");
            }
        }
    }

    /// <inheritdoc />
    public async Task<bool> CheckIfUserExistsAsync(string email)
    {
        return await this.userManager.FindByEmailAsync(email) != null;
    }

    /// <inheritdoc />
    public async Task<bool> CheckPasswordAsync(string email, string password)
    {
        var user = await this.userManager.FindByEmailAsync(email);

        return user is not null && await this.userManager.CheckPasswordAsync(user, password);
    }

    public async Task RemoveRoleFromUserAsync(string userId, string roleType)
    {
        var user = await this.userManager.FindByIdAsync(userId);
        if (user is null)
        {
            throw new ArgumentException("Invalid ID of the user");
        }

        var userRoles = await this.userManager.GetRolesAsync(user);
        if (userRoles.Contains(roleType))
        {
            var result = await this.userManager.RemoveFromRoleAsync(user, roleType);
            if (!result.Succeeded)
            {
                throw new Exception($"Removing user from \"{roleType}\" role failed");
            }
        }
    }
}