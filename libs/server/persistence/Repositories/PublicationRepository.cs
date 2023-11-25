using Kathanika.Application.Services;

namespace Kathanika.Infrastructure.Persistence.Repositories;

internal sealed class PublicationRepository : Repository<Publication>, IPublicationRepository
{
    private const string collectionName = "publications";
    public PublicationRepository(IMongoDatabase database, ILogger<PublicationRepository> logger, ICacheService cacheService) : base(database, collectionName, logger, cacheService)
    {
    }

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
