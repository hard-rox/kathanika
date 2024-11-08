using Kathanika.Application.Services;
using Kathanika.Domain.Aggregates.PatronAggregate;

namespace Kathanika.Infrastructure.Persistence.Repositories;

internal sealed class PatronRepository(
    IMongoDatabase database,
    ILogger<PatronRepository> logger,
    ICacheService cacheService)
    : Repository<Patron>(database, CollectionName, logger, cacheService), IPatronRepository
{
    private const string CollectionName = "patrons";
}
