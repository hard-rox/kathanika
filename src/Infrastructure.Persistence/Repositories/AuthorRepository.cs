using Kathanika.Application.Abstractions;
using Kathanika.Application.Services;
using Kathanika.Infrastructure.Persistence.Implementations;

namespace Kathanika.Infrastructure.Persistence.Repositories;

internal sealed class AuthorRepository : Repository<Author>, IAuthorRepository
{
    private const string collectionName = "authors";
    public AuthorRepository(IUnitOfWork unitOfWork, IMongoDatabase database, ILogger<AuthorRepository> logger, ICacheService cacheService)
        : base(unitOfWork, database, collectionName, logger, cacheService)
    {
    }
}