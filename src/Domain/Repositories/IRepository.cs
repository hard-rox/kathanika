namespace Kathanika.Domain.Repositories;

public interface IRepository<T> where T : class
{
    // Task<T> GetByIdAsync(Guid id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T> AddAsync(T entity);
    // Task UpdateAsync(T entity);
    // Task DeleteAsync(T entity);
}