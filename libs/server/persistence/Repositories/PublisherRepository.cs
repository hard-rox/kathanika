using Kathanika.Application.Services;

namespace Kathanika.Infrastructure.Persistence.Repositories;

internal sealed class PublisherRepository(IMongoDatabase database, ILogger<IPublisherRepository> logger, ICacheService cacheService) : Repository<Publisher>(database, collectionName, logger, cacheService), IPublisherRepository
{
    private const string collectionName = "publishers";
}
