
using ISO810_ERP.Config;
using Microsoft.Extensions.Caching.Distributed;

namespace ISO810_ERP.Services;

public class JwtTokenBlackListCache
{
    private const string BLACKLIST_KEY = "jwt-token-blacklist";
    private readonly IDistributedCache cache;

    public JwtTokenBlackListCache(IDistributedCache cache)
    {
        this.cache = cache;
    }

    public void Add(string token)
    {
        cache.SetString(GetTokenKey(token), token, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = AppSettings.JwtDuration,
        });
    }

    public void Remove(string token)
    {
        cache.Remove(GetTokenKey(token));
    }

    public bool Contains(string token)
    {
        return cache.GetString(GetTokenKey(token)) != null;
    }

    private static string GetTokenKey(string token)
    {
        return $"{BLACKLIST_KEY}-{token}";
    }
}