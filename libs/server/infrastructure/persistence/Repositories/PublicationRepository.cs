using Kathanika.Core.Application.Services;
using Kathanika.Core.Domain.Aggregates.PublicationAggregate;

namespace Kathanika.Infrastructure.Persistence.Repositories;

internal sealed class PublicationRepository(IMongoDatabase database, ILogger<PublicationRepository> logger, ICacheService cacheService) : Repository<Publication>(database, collectionName, logger, cacheService), IPublicationRepository
{
    private const string collectionName = "publications";

    public async Task<IReadOnlyList<Publication>> ListAllByAuthorIdAsync(string authorId, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting all Publications with author ID {@AuthorId}", authorId);
        FilterDefinition<Publication> filter = Builders<Publication>.Filter
            .ElemMatch(x => x.Authors,
                Builders<PublicationAuthor>.Filter.Eq(x => x.Id, authorId));
        List<Publication> filteredPublications = await _collection.Find(filter)
            .ToListAsync(cancellationToken);

        return filteredPublications;
    }
}
