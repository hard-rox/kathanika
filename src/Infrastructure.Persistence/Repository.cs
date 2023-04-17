using Kathanika.Domain.Premitives;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Kathanika.Infrastructure.Persistence;

internal abstract class Repository<T> : IRepository<T> where T : AggregateRoot
{
    private readonly string _collectionName = string.Empty;
    private readonly IMongoCollection<T> _collection;
    private readonly ILogger<IRepository<T>> _logger;

    public Repository(IMongoDatabase database, string collectionName, ILogger<IRepository<T>> logger)
    {
        _collectionName = collectionName.ToLower();
        _collection = database.GetCollection<T>(_collectionName);
        _logger = logger;
    }

    public IQueryable<T> Get()
    {
        return _collection.AsQueryable<T>();
    }

    public async Task<T> GetByIdAsync(string id)
    {
        _logger.LogInformation("Getting document of type {@DocumentType} with id {@DocumentId} from {CollectionName}", typeof(T).Name, id, _collectionName);
        var document = await _collection.Find(x => x.Id == id).SingleOrDefaultAsync();
        _logger.LogInformation("Got document {@Document} of type {@DocumentType} from {CollectionName}", document, typeof(T).Name, _collectionName);
        return document;
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        _logger.LogInformation("Getting all documents of type {@DocumentType} from collection {@CollectionName}", typeof(T).Name, _collectionName);
        var list = await _collection.Find(_ => true).ToListAsync();
        return list;
    }

    public async Task<T> AddAsync(T entity)
    {
        _logger.LogInformation("Adding new document {@Document} of type {@DocumentType} into collection {@CollectionName}", entity, typeof(T).Name, _collectionName);
        await _collection.InsertOneAsync(entity);
        _logger.LogInformation("Added new document with _id {@_id} of type {@DocumentType} into collection {@CollectionName}", entity.ToBsonDocument()["_id"].ToJson(), typeof(T).Name, _collectionName);
        return entity;
    }

    public async Task UpdateAsync(T entity)
    {
        _logger.LogInformation("Updating document of type {@DocumentType} with id {@DocumentId} from {CollectionName} with value {@NewValue}",
        typeof(T).Name, entity.Id, _collectionName, entity);
        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(entity.Id));
        await _collection.ReplaceOneAsync(filter, entity);
        _logger.LogInformation("Updated document of type {@DocumentType} with id {@DocumentId} from {CollectionName} with value {@NewValue}",
        typeof(T).Name, entity.Id, _collectionName, entity);
    }

    public async Task DeleteAsync(string id)
    {
        _logger.LogInformation("Deleting document of type {@DocumentType} with id {@DocumentId} from {CollectionName}", typeof(T).Name, id, _collectionName);
        var filter = Builders<T>.Filter.Eq("_id", ObjectId.Parse(id));
        await _collection.DeleteOneAsync(filter);
        _logger.LogInformation("Deleted document of type {@DocumentType} with id {@DocumentId} from {CollectionName}", typeof(T).Name, id, _collectionName);
    }
}