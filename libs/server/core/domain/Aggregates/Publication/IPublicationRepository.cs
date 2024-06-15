using Kathanika.Core.Domain.Primitives;

namespace Kathanika.Core.Domain.Aggregates;

public interface IPublicationRepository : IRepository<Publication>
{
    public Task<IReadOnlyList<Publication>> ListAllByAuthorIdAsync(string authorId, CancellationToken cancellationToken = default);
}
