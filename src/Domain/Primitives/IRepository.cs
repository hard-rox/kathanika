using System.Linq.Expressions;

namespace Kathanika.Domain.Primitives;

public interface IRepository<T> where T : AggregateRoot
{
    IQueryable<T> AsQueryable();
    Task<T> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default);
    Task<IReadOnlyList<T>> ListAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    Task<long> CountAsync(CancellationToken cancellationToken = default);
    Task<long> CountAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    Task<T> AddAsync(T aggregate, CancellationToken cancellationToken = default);
    Task UpdateAsync(T aggregate, CancellationToken cancellationToken = default);
    Task DeleteAsync(string id, CancellationToken cancellationToken = default);
}