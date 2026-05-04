using InnoclinicAutho.Application.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace InnoclinicAutho.Infrastructure.Caching;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;
    private readonly JsonSerializerOptions _serializerOptions;

    public CacheService(
        IDistributedCache cache,
        JsonSerializerOptions serializerOptions)
    {
        _cache = cache;
        _serializerOptions = serializerOptions;
    }

    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        var cached = await _cache.GetAsync(key, cancellationToken);
        
        if (cached == null || cached.Length == 0)
            return null;

        var json = Encoding.UTF8.GetString(cached);

        return JsonSerializer.Deserialize<T>(json, _serializerOptions);
    }

    public async Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        var cached = await _cache.GetAsync(key, cancellationToken);

        if (cached == null || cached.Length == 0)
            return;

        await _cache.RemoveAsync(key, cancellationToken);
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiration = null, CancellationToken cancellationToken = default) where T : class
    {
        var options = new DistributedCacheEntryOptions();

        if (expiration.HasValue)
            options.SetSlidingExpiration(expiration.Value);
        else
            options.SetSlidingExpiration(TimeSpan.FromMinutes(30));

        var serialized = JsonSerializer.Serialize(value, _serializerOptions);
        await _cache.SetStringAsync(key, serialized, options, cancellationToken);
    }

    public async Task<bool> KeyExistsAsync(string key, CancellationToken cancellationToken = default)
    {
        var cached = await _cache.GetAsync(key, cancellationToken);

        if (cached == null || cached.Length == 0)
            return false;

        return true;
    }
}
