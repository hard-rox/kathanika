using Kathanika.Domain.Aggregates.PurchaseOrderAggregate;
using Microsoft.Extensions.Caching.Hybrid;

namespace Kathanika.Infrastructure.Persistence.Repositories;

internal sealed class PurchaseOrderRepository(
    IMongoDatabase database,
    ILogger<PurchaseOrderRepository> logger,
    HybridCache hybridCache)
    : Repository<PurchaseOrder>(database, CollectionName, logger, hybridCache), IPurchaseOrderRepository
{
    private const string CollectionName = "purchaseRequests";
}