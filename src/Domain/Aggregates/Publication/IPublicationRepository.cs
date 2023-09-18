using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates;

public interface IPublicationRepository : IRepository<Publication>
{
    public Task<IReadOnlyList<Publication>> ListAllByAuthorIdAsync(string authorId, CancellationToken cancellationToken = default);
}
