using Kathanika.Core.Application.Services;

namespace Kathanika.Infrastructure.Persistence.Repositories;

internal sealed class PatronRepository(
    IMongoDatabase database,
    ILogger<PatronRepository> logger,
    ICacheService cacheService)
    : Repository<Patron>(database, collectionName, logger, cacheService), IPatronRepository
{
    private const string collectionName = "patrons";
}
