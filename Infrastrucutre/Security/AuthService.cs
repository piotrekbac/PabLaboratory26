using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using AppCore.Authorization;
using AppCore.DTOs;
using AppCore.Exceptions;
using AppCore.Interfaces;
using AppCore.Models;
using Infrastructure.EntityFramework.Context;
using Infrastructure.EntityFramework.Entities;
using Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Security;

public class AuthService(
    UserManager<CrmUser> userManager,
    ContactsDbContext context,
    JwtSettings jwtOptions) : IAuthService
{
    public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await userManager.FindByEmailAsync(dto.Email)
            ?? throw new Exception("Nieprawidłowy email lub hasło.");

        if (!await userManager.CheckPasswordAsync(user, dto.Password))
            throw new Exception("Nieprawidłowy email lub hasło.");

        if (user.Status != SystemUserStatus.Active)
            throw new Exception("Konto jest nieaktywne.");

        return await GenerateAuthResponseAsync(user);
    }

    public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto dto)
    {
        var principal = GetPrincipalFromExpiredToken(dto.AccessToken);
        var userId = principal.FindFirstValue(ClaimTypes.NameIdentifier) 
                     ?? throw new Exception("Nieprawidłowy token.");
        
        var user = await userManager.FindByIdAsync(userId) 
                   ?? throw new Exception("Użytkownik nie istnieje.");

        var refreshToken = await context.RefreshTokens
            .FirstOrDefaultAsync(t => t.Token == dto.RefreshToken && t.UserId == userId)
            ?? throw new Exception("Nieprawidłowy refresh token.");

        if (!refreshToken.IsActive) throw new Exception("Token wygasł lub został odwołany.");

        var newResponse = await GenerateAuthResponseAsync(user);
        refreshToken.Revoke(newResponse.RefreshToken);
        await context.SaveChangesAsync();

        return newResponse;
    }

    public async Task RevokeTokenAsync(string refreshToken)
    {
        var token = await context.RefreshTokens.FirstOrDefaultAsync(t => t.Token == refreshToken)
            ?? throw new Exception("Token nie istnieje.");
        token.Revoke();
        await context.SaveChangesAsync();
    }

    private async Task<AuthResponseDto> GenerateAuthResponseAsync(CrmUser user)
    {
        var roles = await userManager.GetRolesAsync(user);
        var accessToken = GenerateAccessToken(user, roles);
        var refreshToken = await GenerateRefreshTokenAsync(user.Id);

        return new AuthResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken.Token,
            ExpiresAt = DateTime.UtcNow.AddMinutes(jwtOptions.ExpirationInMinutes),
            User = new UserDto
            {
                Id = user.Id,
                Email = user.Email!,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Department = user.Department,
                Status = user.Status,
                Roles = roles
            }
        };
    }

    private string GenerateAccessToken(CrmUser user, IList<string> roles)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.GivenName, user.FirstName),
            new(ClaimTypes.Surname, user.LastName),
            new("department", user.Department),
            new("status", user.Status.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

        var token = new JwtSecurityToken(
            jwtOptions.Issuer, jwtOptions.Audience, claims,
            DateTime.UtcNow, DateTime.UtcNow.AddMinutes(jwtOptions.ExpirationInMinutes),
            new SigningCredentials(jwtOptions.GetSymmetricKey(), SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task<RefreshToken> GenerateRefreshTokenAsync(string userId)
    {
        var token = new RefreshToken
        {
            UserId = userId,
            Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
            ExpiresAt = DateTime.UtcNow.AddDays(jwtOptions.RefreshTokenDays)
        };
        await context.RefreshTokens.AddAsync(token);
        await context.SaveChangesAsync();
        return token;
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string accessToken)
    {
        var handler = new JwtSecurityTokenHandler();
        return handler.ValidateToken(accessToken, new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = jwtOptions.GetSymmetricKey(),
            ValidateLifetime = false
        }, out _);
    }
}