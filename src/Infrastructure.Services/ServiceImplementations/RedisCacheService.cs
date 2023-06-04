using Kathanika.Application.Services;

namespace Kathanika.Infrastructure.Services.ServiceImplementations;

internal class RedisCacheService : ICacheService
{
    public T? Get<T>(string key)
    {
        throw new NotImplementedException();
    }

    public void Remove(string key)
    {
        throw new NotImplementedException();
    }

    public void Set<T>(string key, T value, TimeSpan? expirationTime = null)
    {
        throw new NotImplementedException();
    }
}
