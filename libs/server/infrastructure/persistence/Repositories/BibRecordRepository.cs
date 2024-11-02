using Kathanika.Core.Application.Services;
using Kathanika.Core.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Infrastructure.Persistence.Repositories;

internal sealed class BibRecordRepository(IMongoDatabase database,
    ILogger<BibRecordRepository> logger,
    ICacheService cacheService)
    : Repository<BibRecord>(database, collectionName, logger, cacheService), IBibRecordRepository
{
    private const string collectionName = "bibRecords";
}
