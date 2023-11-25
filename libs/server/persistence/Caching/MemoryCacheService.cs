using Kathanika.Application.Services;
using Microsoft.Extensions.Caching.Memory;

namespace Kathanika.Infrastructure.Persistence.Caching;

internal sealed class MemoryCacheService : ICacheService
{
    private readonly TimeSpan defaultExpirationTime = TimeSpan.FromSeconds(120);
    private readonly IMemoryCache memoryCache;

    public MemoryCacheService(IMemoryCache memoryCache)
    {
        this.memoryCache = memoryCache;
    }

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
        memoryCache.Set<T>(key, value, expirationTime ?? defaultExpirationTime);
    }
}
