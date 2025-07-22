using Kathanika.Domain.Aggregates.BibItemAggregate;
using Microsoft.Extensions.Caching.Hybrid;

namespace Kathanika.Infrastructure.Persistence.Repositories;

internal sealed class BibItemRepository(
    IMongoDatabase database,
    ILogger<BibItemRepository> logger,
    HybridCache hybridCache)
    : Repository<BibItem>(database, CollectionName, logger, hybridCache), IBibItemRepository
{
    private const string CollectionName = "bibItems";
}