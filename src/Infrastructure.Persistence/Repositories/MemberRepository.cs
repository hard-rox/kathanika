using Kathanika.Application.Services;

namespace Kathanika.Infrastructure.Persistence.Repositories;

internal sealed class MemberRepository : Repository<Member>, IMemberRepository
{
    private const string collectionName = "members";
    public MemberRepository(IMongoDatabase database, ILogger<MemberRepository> logger, ICacheService cacheService) : base(database, collectionName, logger, cacheService)
    {
    }
}
