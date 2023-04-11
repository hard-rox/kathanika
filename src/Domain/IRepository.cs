namespace Kathanika.Domain;

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(string id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(string id, T entity);
    Task DeleteAsync(string id);
}