using Kathanika.Domain.Aggregates.Book;
using Kathanika.Domain.Repositories;
using MongoDB.Driver;

namespace Kathanika.Infrastructure.Persistence.Repositories;

internal class BookRepository : Repository<Book>, IBookRepository
{
    private const string _collectionName = "books";
    public BookRepository(IMongoDatabase database) : base(database, _collectionName)
    {
    }
}
