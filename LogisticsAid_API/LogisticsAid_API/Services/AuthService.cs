using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using LogisticsAid_API.DTOs;
using Microsoft.IdentityModel.Tokens;

namespace LogisticsAid_API.Services;

public class AuthService
{
    public string GenerateToken(LogisticianDTO logistician)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = "JwtKeyIGuessJwtKeyIGuessJwtKeyIGuessJwtKeyIGuess"u8.ToArray();

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, logistician.ContactInfo.Id.ToString()), // Використовуємо Id
            new("hasAdminPrivileges", logistician.HasAdminPrivileges.ToString()) // Додаємо інформацію про права
        };

        if (!string.IsNullOrEmpty(logistician.ContactInfo.Email))
        {
            claims.Add(new(JwtRegisteredClaimNames.Email, logistician.ContactInfo.Email)); // Додаємо email, якщо є
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddHours(12),
            Issuer = "https://logisticsaid.com",
            Audience = "https://logisticsaid.com",
            SigningCredentials =
                new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public CookieOptions GetCookieOptions()
    {
        return new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Lax,
            Path = "/",
            Expires = DateTime.UtcNow.AddHours(12)
        };
    }
}