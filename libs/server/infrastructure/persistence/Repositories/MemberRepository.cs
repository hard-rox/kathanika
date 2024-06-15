using Kathanika.Core.Application.Services;

namespace Kathanika.Infrastructure.Persistence.Repositories;

internal sealed class MemberRepository(IMongoDatabase database, ILogger<MemberRepository> logger, ICacheService cacheService) : Repository<Member>(database, collectionName, logger, cacheService), IMemberRepository
{
    private const string collectionName = "members";
}
