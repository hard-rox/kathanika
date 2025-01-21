using Kathanika.Domain.Aggregates.VendorAggregate;
using Microsoft.Extensions.Caching.Hybrid;

namespace Kathanika.Infrastructure.Persistence.Repositories;

internal sealed class VendorRepository(
    IMongoDatabase database,
    ILogger<VendorRepository> logger,
    HybridCache hybridCache)
    : Repository<Vendor>(database, CollectionName, logger, hybridCache), IVendorRepository
{
    private const string CollectionName = "vendors";
}