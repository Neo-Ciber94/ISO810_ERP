
using System;

namespace ISO810_ERP.Config;

public static class AppSettings
{
    private static string? _jwtSecret;

    public static TimeSpan JwtDuration { get; } = TimeSpan.FromDays(1);

    public static string JwtSecret
    {
        get
        {
            if (_jwtSecret == null)
            {
                _jwtSecret = Environment.GetEnvironmentVariable("ISO810_JWT_SECRET") ?? throw new Exception("ISO810_JWT_SECRET is not set");
            }

            return _jwtSecret;
        }
    }
}