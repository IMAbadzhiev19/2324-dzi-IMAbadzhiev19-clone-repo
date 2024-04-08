using AutoMapper;
using Microsoft.AspNetCore.Identity;
using PMS.Data.Models.Auth;
using PMS.Services.Contracts.Auth;
using PMS.Shared.Models.User;

namespace PMS.Services.Implementations.Auth;

/// <summary>
/// A class responsible for user-related operations.
/// </summary>
internal class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IMapper mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserService"/> class.
    /// </summary>
    /// <param name="userManager">User manager.</param>
    /// <param name="mapper">Auto mapper.</param>
    public UserService(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        this.userManager = userManager;
        this.mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<UserVM> GetUserByIdAsync(string userId)
    {
        var user = await this.userManager.FindByIdAsync(userId);

        if (user is null)
        {
            throw new ArgumentException("Invalid id");
        }

        return this.mapper.Map<UserVM>(user);
    }

    /// <inheritdoc />
    public async Task UpdateUserInfoAsync(string id, UserUM userUM)
    {
        var user = await this.userManager.FindByIdAsync(id);

        if (user is null)
        {
            throw new ArgumentException("Invalid id");
        }

        user.FirstName = userUM.FirstName;
        user.LastName = userUM.LastName;
        user.Email = userUM.Email;
        user.PhoneNumber = userUM.PhoneNumber;

        await this.userManager.UpdateAsync(user);
    }

    /// <inheritdoc />
    public async Task<IdentityResult> ChangePasswordAsync(string userId, string newPassword)
    {
        var user = await this.userManager.FindByIdAsync(userId);

        if (user is null)
        {
            throw new ArgumentException("Invalid userId");
        }

        var passwordToken = await this.userManager.GeneratePasswordResetTokenAsync(user);
        return await this.userManager.ResetPasswordAsync(user, passwordToken, newPassword);
    }
}