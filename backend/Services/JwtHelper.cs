
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ISO810_ERP.Config;
using ISO810_ERP.Dtos;
using ISO810_ERP.Models;
using Microsoft.IdentityModel.Tokens;

namespace ISO810_ERP.Services;

public static class JwtHelper
{
    public static string GenerateToken(AccountDto account)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(AppSettings.JwtSecret);
        var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
        var expiration = DateTime.UtcNow + AppSettings.JwtDuration;

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Name, account.Name),
                new Claim(ClaimTypes.Email, account.Email)
            }),

            Expires = expiration,
            SigningCredentials = creds
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
    public static AccountDto? ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(AppSettings.JwtSecret);
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false
        };

        try
        {
            var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);
            var id = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var name = principal.FindFirst(ClaimTypes.Name)?.Value;
            var email = principal.FindFirst(ClaimTypes.Email)?.Value;

            if (id == null || name == null || email == null)
            {
                return null;
            }

            return new AccountDto
            {
                Id = int.Parse(id),
                Name = name,
                Email = email
            };
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static bool IsValid(string token)
    {
        return ValidateToken(token) != null;
    }
}