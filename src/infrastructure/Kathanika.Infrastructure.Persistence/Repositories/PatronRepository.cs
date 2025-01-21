using Kathanika.Domain.Aggregates.PatronAggregate;
using Microsoft.Extensions.Caching.Hybrid;

namespace Kathanika.Infrastructure.Persistence.Repositories;

internal sealed class PatronRepository(
    IMongoDatabase database,
    ILogger<PatronRepository> logger,
    HybridCache hybridCache)
    : Repository<Patron>(database, CollectionName, logger, hybridCache), IPatronRepository
{
    private const string CollectionName = "patrons";
}