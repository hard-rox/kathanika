namespace Kathanika.Domain.Premitives;

public interface IRepository<T> where T : AggregateRoot
{
    IQueryable<T> Get();
    Task<T> GetByIdAsync(string id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(string id);
}