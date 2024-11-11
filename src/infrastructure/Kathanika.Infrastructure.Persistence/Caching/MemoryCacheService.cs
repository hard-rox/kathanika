using Kathanika.Application.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Kathanika.Infrastructure.Persistence.Caching;

internal sealed class MemoryCacheService(IMemoryCache memoryCache) : ICacheService
{
    private readonly TimeSpan _defaultExpirationTime = TimeSpan.FromSeconds(120);

    public T? Get<T>(string key)
    {
        T? item = memoryCache.Get<T>(key);
        return item;
    }

    public void Remove(string key)
    {
        memoryCache.Remove(key);
    }

    public void Set<T>(string key, T value, TimeSpan? expirationTime = null)
    {
        memoryCache.Set(key, value, expirationTime ?? _defaultExpirationTime);
    }
}