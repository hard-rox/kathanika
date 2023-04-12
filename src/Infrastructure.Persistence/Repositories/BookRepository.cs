using Kathanika.Domain.Aggregates.Book;
using Kathanika.Domain.Repositories;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace Kathanika.Infrastructure.Persistence.Repositories;

internal class BookRepository : Repository<Book>, IBookRepository
{
    private const string _collectionName = "books";
    public BookRepository(IMongoDatabase database, ILogger<BookRepository> logger) : base(database, _collectionName, logger)
    {
    }
}
