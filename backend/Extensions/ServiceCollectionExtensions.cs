using System;
using System.Text;
using System.Threading.Tasks;
using ISO810_ERP.Config;
using ISO810_ERP.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ISO810_ERP.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.JwtSecret));
            options.TokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = key,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            options.Events = new JwtBearerEvents
            {
                // We capture the token from the cookies
                OnMessageReceived = context =>
                {
                    context.Token = context.Request.Cookies[AppSettings.JwtCookieName];
                    return Task.CompletedTask;
                },

                // Check if the token is not blacklisted
                OnTokenValidated = context =>
                {
                    using var scope = context.HttpContext.RequestServices.CreateScope();
                    var cache = scope.ServiceProvider.GetService<TypedCache>()!;

                    // SAFETY: If the token was validated must be non-null
                    var token = context.HttpContext.Request.Cookies[AppSettings.JwtCookieName]!;

                    if (cache.Contains(Constants.BlackListedTokenTag, token))
                    {
                        context.Fail("Invalid token");
                    }

                    return Task.CompletedTask;
                }
            };
        });

        return services;
    }
}