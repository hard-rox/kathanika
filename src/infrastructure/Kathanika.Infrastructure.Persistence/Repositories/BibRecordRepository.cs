using Kathanika.Domain.Aggregates.BibRecordAggregate;
using Microsoft.Extensions.Caching.Hybrid;

namespace Kathanika.Infrastructure.Persistence.Repositories;

internal sealed class BibRecordRepository(
    IMongoDatabase database,
    ILogger<BibRecordRepository> logger,
    HybridCache hybridCache)
    : Repository<BibRecord>(database, CollectionName, logger, hybridCache), IBibRecordRepository
{
    private const string CollectionName = "bibRecords";
}