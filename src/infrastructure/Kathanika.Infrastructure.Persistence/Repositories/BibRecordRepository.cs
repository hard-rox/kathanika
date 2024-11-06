using Kathanika.Application.Services;
using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Infrastructure.Persistence.Repositories;

internal sealed class BibRecordRepository(IMongoDatabase database,
    ILogger<BibRecordRepository> logger,
    ICacheService cacheService)
    : Repository<BibRecord>(database, CollectionName, logger, cacheService), IBibRecordRepository
{
    private const string CollectionName = "bibRecords";
}
