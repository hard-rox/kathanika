using Kathanika.Application.Services;

namespace Kathanika.Infrastructure.Persistence.Repositories;

internal sealed class AuthorRepository : Repository<Author>, IAuthorRepository
{
    private const string collectionName = "authors";
    public AuthorRepository(IMongoDatabase database, ILogger<AuthorRepository> logger, ICacheService cacheService) : base(database, collectionName, logger, cacheService)
    {
    }
}