
using System.Text.Json;
using System.Threading.Tasks;
using ISO810_ERP.Utils;
using Microsoft.Extensions.Caching.Distributed;

namespace ISO810_ERP.Services;

public class TypedCache
{
    private readonly IDistributedCache cache;

    public TypedCache(IDistributedCache cache)
    {
        this.cache = cache;
    }

    public void Set<T>(string tag, string key, T value, DistributedCacheEntryOptions? options = null)
    {
        var serialized = JsonSerializer.Serialize(value);
        cache.SetString(GetCacheKey(tag, key), serialized, options);
    }

    public Optional<T> Get<T>(string tag, string key)
    {
        try
        {
            var serialized = cache.GetString(GetCacheKey(tag, key));
            if (serialized == null)
            {
                return Optional.None;
            }

            T? value = JsonSerializer.Deserialize<T>(serialized);

            if (value == null)
            {
                return Optional.None;
            }

            return Optional.Some(value);
        }
        catch
        {
            return Optional.None;
        }
    }

    public T? GetOrNull<T>(string tag, string key) where T : class
    {
        return Get<T>(tag, key).GetValueOrDefault();
    }

    public void Refresh(string tag, string key)
    {
        cache.Refresh(GetCacheKey(tag, key));
    }

    public bool Contains(string tag, string key)
    {
        return cache.GetString(GetCacheKey(tag, key)) != null;
    }

    public async Task<Optional<T>> GetAsync<T>(string tag, string key)
    {
        try
        {
            var serialized = await cache.GetStringAsync(GetCacheKey(tag, key));
            if (serialized == null)
            {
                return Optional.None;
            }

            T? value = JsonSerializer.Deserialize<T>(serialized);

            if (value == null)
            {
                return Optional.None;
            }

            return Optional.Some(value);
        }
        catch
        {
            return Optional.None;
        }
    }

    public async Task<Optional<T>> GetOrNullAsync<T>(string tag, string key) where T : class
    {
        return (await GetAsync<T>(tag, key)).GetValueOrDefault();
    }

    public Task SetAsync<T>(string tag, string key, T value, DistributedCacheEntryOptions? options = null)
    {
        var serialized = JsonSerializer.Serialize(value);
        return cache.SetStringAsync(GetCacheKey(tag, key), serialized, options);
    }

    public Task RefreshAsync(string tag, string key)
    {
        return cache.RefreshAsync(GetCacheKey(tag, key));
    }

    public async Task<bool> ContainsAsync(string tag, string key)
    {
        var result = await cache.GetStringAsync(GetCacheKey(tag, key));
        return result != null;
    }

    private static string GetCacheKey(string tag, string key)
    {
        return $"{tag}:{key}";
    }
}