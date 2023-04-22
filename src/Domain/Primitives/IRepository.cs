using System.Linq.Expressions;

namespace Kathanika.Domain.Primitives;

public interface IRepository<T> where T : AggregateRoot
{
    IQueryable<T> Get();
    Task<T> GetByIdAsync(string id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<IReadOnlyList<T>> ListAllAsync(Expression<Func<T, bool>> expression);
    Task<T> AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(string id);
}