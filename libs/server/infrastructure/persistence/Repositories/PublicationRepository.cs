using Kathanika.Core.Application.Services;

namespace Kathanika.Infrastructure.Persistence.Repositories;

internal sealed class PublicationRepository(IMongoDatabase database, ILogger<PublicationRepository> logger, ICacheService cacheService)
    : Repository<Publication>(database, collectionName, logger, cacheService), IPublicationRepository
{
    private const string collectionName = "publications";
}
