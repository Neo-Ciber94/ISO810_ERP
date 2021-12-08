
using System;
using ISO810_ERP.Config;
using ISO810_ERP.Dtos;
using ISO810_ERP.Services;
using Microsoft.AspNetCore.Http;

namespace ISO810_ERP.Extensions;

public static class HttpContextExtensions
{
    public static string? GetJwtToken(this HttpContext context)
    {
        return context.Request.Cookies[AppSettings.JwtCookieName];
    }

    public static AccountDto? GetCookieUserAccount(this HttpContext context)
    {
        var jwtToken = context.Request.Cookies[AppSettings.JwtCookieName];

        if (jwtToken == null)
        {
            return null;
        }

        var account = JwtHelper.ValidateToken(jwtToken);

        if (account == null)
        {
            return null;
        }

        return account;
    }

    public static void SetCookieUserAccount(this HttpContext context, AccountDto account)
    {
        var jwtToken = JwtHelper.GenerateToken(account);
        var expiration = DateTime.UtcNow + AppSettings.JwtDuration;

        context.Response.Cookies.Append(AppSettings.JwtCookieName, jwtToken, new CookieOptions
        {
            Expires = expiration,
            // HttpOnly = true
        });
    }

    public static void RemoveCookieUserAccount(this HttpContext context)
    {
        context.Response.Cookies.Delete(AppSettings.JwtCookieName);
    }
}