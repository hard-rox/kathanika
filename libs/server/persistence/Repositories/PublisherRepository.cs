using Kathanika.Application.Services;

namespace Kathanika.Infrastructure.Persistence.Repositories;

internal sealed class PublisherRepository : Repository<Publisher>, IPublisherRepository
{
    private const string collectionName = "publishers";
    public PublisherRepository(IMongoDatabase database, ILogger<IPublisherRepository> logger, ICacheService cacheService) : base(database, collectionName, logger, cacheService)
    {
    }
}
