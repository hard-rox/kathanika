namespace Kathanika.Application.Services;

public interface ICacheService
{
    public T? Get<T>(string key);
    public void Set<T>(string key, T value, TimeSpan? expirationTime = null);
    public void Remove(string key);
}
