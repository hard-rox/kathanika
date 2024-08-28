using Kathanika.Core.Application.Services;
using Kathanika.Core.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Infrastructure.Persistence.Repositories;

internal sealed class VendorRepository(IMongoDatabase database, ILogger<VendorRepository> logger, ICacheService cacheService)
    : Repository<Vendor>(database, collectionName, logger, cacheService), IVendorRepository
{
    private const string collectionName = "vendors";
}
