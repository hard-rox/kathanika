using Kathanika.Application.Services;

namespace Kathanika.Persistence.Repositories;

internal sealed class MemberRepository(IMongoDatabase database, ILogger<MemberRepository> logger, ICacheService cacheService) : Repository<Member>(database, collectionName, logger, cacheService), IMemberRepository
{
    private const string collectionName = "members";
}
