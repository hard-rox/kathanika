using Kathanika.Application.Abstractions;
using Kathanika.Application.Services;
using Kathanika.Infrastructure.Persistence.Implementations;

namespace Kathanika.Infrastructure.Persistence.Repositories;

internal sealed class PublicationRepository : Repository<Publication>, IPublicationRepository
{
    private const string collectionName = "publications";
    public PublicationRepository(IUnitOfWork unitOfWork, IMongoDatabase database, ILogger<PublicationRepository> logger, ICacheService cacheService)
    : base(unitOfWork, database, collectionName, logger, cacheService)
    {
    }
}
