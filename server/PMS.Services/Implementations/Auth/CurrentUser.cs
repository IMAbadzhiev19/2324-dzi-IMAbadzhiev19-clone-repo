using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using PMS.Shared.Contracts;

namespace PMS.Services.Implementations.Auth;

/// <summary>
/// A class responsible for extraction current user ID.
/// </summary>
internal class CurrentUser : ICurrentUser
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CurrentUser"/> class.
    /// </summary>
    /// <param name="httpContextAccessor">Http context accessor.</param>
    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        this.UserId = httpContextAccessor?
            .HttpContext?
            .User?
            .Claims
            .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?
            .Value!;
    }

    /// <inheritdoc />
    public string UserId { get; }
}