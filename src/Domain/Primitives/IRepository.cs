using System.Linq.Expressions;

namespace Kathanika.Domain.Primitives;

public interface IRepository<T> where T : AggregateRoot
{
    IQueryable<T> AsQueryable();
    Task<T> GetByIdAsync(string id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<IReadOnlyList<T>> ListAllAsync(Expression<Func<T, bool>> expression);
    Task<T> AddAsync(T aggregate);
    Task UpdateAsync(T aggregate);
    Task DeleteAsync(string id);
}