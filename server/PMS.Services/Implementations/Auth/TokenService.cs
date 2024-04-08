using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PMS.Data;
using PMS.Data.Models.Auth;
using PMS.Services.Contracts.Auth;
using PMS.Shared.Models.User.Auth.Token;
using PMS.Shared.Options;

namespace PMS.Services.Implementations.Auth;

/// <summary>
/// A class responsible for token-related operations.
/// </summary>
internal class TokenService : ITokenService
{
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ApplicationDbContext context;
    private readonly TokensOptions options;

    /// <summary>
    /// Initializes a new instance of the <see cref="TokenService"/> class.
    /// </summary>
    /// <param name="userManager">The user manager.</param>
    /// <param name="context">The application database context.</param>
    /// <param name="options">The token options.</param>
    public TokenService(UserManager<ApplicationUser> userManager, ApplicationDbContext context, IOptions<TokensOptions> options)
    {
        this.userManager = userManager;
        this.context = context;
        this.options = options.Value;
    }

    /// <inheritdoc />
    public async Task<Tokens> CreateNewTokensAsync(TokensIM tokens)
    {
        var principals = this.GetPrincipalsFromExpiredToken(tokens.AccessToken);
        if (principals is null)
        {
            throw new ArgumentException("Invalid access token");
        }

        var user = await this.userManager.FindByIdAsync(principals.FindFirst(ClaimTypes.NameIdentifier)?.Value!);
        var refreshToken = await this.context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokens.RefreshToken);

        var claims = principals.Claims.ToList();
        var oldRoles = claims.Where(c => c.Type == ClaimTypes.Role)
            .Select(c => c.Value)
            .ToList();
        var userRoles = await this.userManager.GetRolesAsync(user!);

        foreach (var role in userRoles)
        {
            if (!oldRoles.Contains(role))
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
        }

        if (user is null || refreshToken is null || !this.ValidateRefreshToken(tokens.RefreshToken))
        {
            throw new ArgumentException("Invalid refreshToken");
        }

        await this.DeleteRefreshTokenAsync(user.Id);
        var newRefreshToken = this.CreateToken(claims, TokenTypes.RefreshToken);

        await this.SaveRefreshTokenAsync(new RefreshToken
        {
            Id = Guid.NewGuid().ToString(),
            Token = new JwtSecurityTokenHandler().WriteToken(newRefreshToken),
            UserId = user.Id,
        });

        return new ()
        {
            AccessToken = this.CreateToken(claims, TokenTypes.AccessToken),
            RefreshToken = newRefreshToken,
        };
    }

    /// <inheritdoc />
    public async Task<Tokens> CreateTokenForUserAsync(string email)
    {
        var user = await this.userManager.FindByEmailAsync(email);

        if (user is null)
        {
            throw new ArgumentException("Invalid email");
        }

        var userRoles = await this.userManager.GetRolesAsync(user);
        var authClaims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, user.Id),
        };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var accessToken = this.CreateToken(authClaims, TokenTypes.AccessToken);

        var refreshToken = await this.context.RefreshTokens.FirstOrDefaultAsync(r => r.UserId == user.Id);
        if (refreshToken is not null)
        {
            await this.DeleteRefreshTokenAsync(user.Id);
        }

        var newRefreshToken = this.CreateToken(authClaims, TokenTypes.RefreshToken);

        await this.SaveRefreshTokenAsync(new RefreshToken
        {
            Id = Guid.NewGuid().ToString(),
            Token = new JwtSecurityTokenHandler().WriteToken(newRefreshToken),
            UserId = user.Id,
        });

        return new ()
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken,
        };
    }

    /// <inheritdoc />
    public async Task DeleteRefreshTokenAsync(string userId)
    {
        var refreshToken = await this.context
            .RefreshTokens
            .FirstOrDefaultAsync(x => x.UserId == userId);

        if (refreshToken is null)
        {
            throw new ArgumentException("Invalid userId");
        }

        this.context.Remove(refreshToken);
        await this.context.SaveChangesAsync();
    }

    /// <inheritdoc />
    public async Task SaveRefreshTokenAsync(RefreshToken refreshToken)
    {
        await this.context.AddAsync(refreshToken);
        await this.context.SaveChangesAsync();
    }

    private JwtSecurityToken CreateToken(List<Claim> authClaims, TokenTypes tokenType)
    {
        SymmetricSecurityKey? signingKey;
        int tokenValidity;

        if (tokenType == TokenTypes.AccessToken)
        {
            signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.options.AccessTokenSecret));
            _ = int.TryParse(this.options.AccessTokenValidityInMinutes, out tokenValidity);
        }
        else
        {
            signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.options.RefreshTokenSecret));
            _ = int.TryParse(this.options.RefreshTokenValidityInDays, out tokenValidity);
        }

        var token = new JwtSecurityToken(
            expires: tokenType == TokenTypes.AccessToken ? DateTime.UtcNow.AddMinutes(tokenValidity) : DateTime.UtcNow.AddDays(tokenValidity),
            claims: authClaims,
            signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256));

        return token;
    }

    private ClaimsPrincipal? GetPrincipalsFromExpiredToken(string? token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.options.AccessTokenSecret)),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = false,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken
            || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new SecurityTokenException("Invalid token");
        }

        return principal;
    }

    private bool ValidateRefreshToken(string refreshToken)
    {
        var tokenValidationParams = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.options.RefreshTokenSecret)),
            ValidateLifetime = true,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken securityToken;
        _ = tokenHandler.ValidateToken(refreshToken, tokenValidationParams, out securityToken);

        if (securityToken is not JwtSecurityToken jwtSecurityToken
            || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
        {
            return false;
        }

        return true;
    }
}