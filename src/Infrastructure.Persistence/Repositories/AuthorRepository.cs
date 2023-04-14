namespace Kathanika.Infrastructure.Persistence.Repositories;

internal class AuthorRepository : Repository<Author>, IAuthorRepository
{
    private const string collectionName = "authors";
    public AuthorRepository(IMongoDatabase database, ILogger<AuthorRepository> logger) : base(database, collectionName, logger)
    {
    }
}