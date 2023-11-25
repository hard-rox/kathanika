using Kathanika.Application.Services;

namespace Kathanika.Infrastructure.Persistence.Repositories;

internal sealed class AuthorRepository(IMongoDatabase database, ILogger<AuthorRepository> logger, ICacheService cacheService) : Repository<Author>(database, collectionName, logger, cacheService), IAuthorRepository
{
    private const string collectionName = "authors";
}
