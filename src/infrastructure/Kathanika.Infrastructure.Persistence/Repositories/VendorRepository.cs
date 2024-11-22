using Kathanika.Application.Services;
using Kathanika.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Infrastructure.Persistence.Repositories;

internal sealed class VendorRepository(
    IMongoDatabase database,
    ILogger<VendorRepository> logger,
    ICacheService cacheService)
    : Repository<Vendor>(database, CollectionName, logger, cacheService), IVendorRepository
{
    private const string CollectionName = "vendors";
}