using Kathanika.Application.Services;
using Kathanika.Domain.Primitives;
using MongoDB.Bson;
using System.Linq.Expressions;

namespace Kathanika.Infrastructure.Persistence;

internal abstract class Repository<T> : IRepository<T> where T : AggregateRoot
{
    private readonly string _collectionName = string.Empty;
    private readonly IMongoCollection<T> _collection;
    private readonly ILogger<IRepository<T>> _logger;
    private readonly ICacheService _cacheService;

    public Repository(IMongoDatabase database, string collectionName, ILogger<IRepository<T>> logger, ICacheService cacheService)
    {
        _collectionName = collectionName.ToLower();
        _collection = database.GetCollection<T>(_collectionName);
        _logger = logger;
        _cacheService = cacheService;
    }

    public IQueryable<T> Get()
    {
        return _collection.AsQueryable();
    }

    public async Task<T> GetByIdAsync(string id)
    {
        _logger.LogInformation("Getting document of type {@DocumentType} with id {@DocumentId} from {CollectionName}", typeof(T).Name, id, _collectionName);
        
        var cacheKey = $"{typeof(T).Name.ToLower()}-{id}";
        _logger.LogInformation("Trying to get document from cache with cache key: {@CacheKey}", cacheKey);
        var cachedDocument = _cacheService.Get<T>(cacheKey);
        if(cachedDocument is not null)
        {
            _logger.LogInformation("Got document {@Document} of type {@DocumentType} from cache with cache key: {@CacheKey} ",
                cachedDocument, typeof(T).Name, cacheKey);
            return cachedDocument;
        }
        _logger.LogInformation("Document not found in cache with cache key: {@CacheKey}", cacheKey);
        
        var document = await _collection.Find(x => x.Id == id).SingleOrDefaultAsync();
        _logger.LogInformation("Got document {@Document} of type {@DocumentType} from {CollectionName}", document, typeof(T).Name, _collectionName);
        
        _logger.LogInformation("Setting document {@Document} into cache with cache key: {@CacheKey}", document, cacheKey);
        _cacheService.Set(cacheKey, document);

        return document;
    }

    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        _logger.LogInformation("Getting all documents of type {@DocumentType} from collection {@CollectionName}", typeof(T).Name, _collectionName);
        var filter = Builders<T>.Filter.Empty;
        var list = await _collection.Find(filter).ToListAsync();
        return list;
    }

    public async Task<IReadOnlyList<T>> ListAllAsync(Expression<Func<T, bool>> expression)
    {
        _logger.LogInformation("Getting all documents satisfying condition {@Condition} of type {@DocumentType} from collection {@CollectionName}",
            expression.ToJson(),
            typeof(T).Name,
            _collectionName);
        var filter = Builders<T>.Filter.Where(expression);
        var list = await _collection.Find(filter).ToListAsync();
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
            typeof(T).Name,
            entity.Id,
            _collectionName,
            entity);

        var filter = Builders<T>.Filter.Eq(x => x.Id, entity.Id);
        await _collection.ReplaceOneAsync(filter, entity);
        _logger.LogInformation("Updated document of type {@DocumentType} with id {@DocumentId} from {CollectionName} with value {@NewValue}",
        typeof(T).Name, entity.Id, _collectionName, entity);

        var cacheKey = $"{typeof(T).Name.ToLower()}-{entity.Id}";
        var cachedDocument = _cacheService.Get<T>(cacheKey);
        if(cachedDocument is not null )
        {
            _logger.LogInformation("Found updating document in cache with key {@CacheKey}. Updating cached document.", cacheKey);
            _cacheService.Set(cacheKey, entity);
        }
    }

    public async Task DeleteAsync(string id)
    {
        _logger.LogInformation("Deleting document of type {@DocumentType} with id {@DocumentId} from {CollectionName}", typeof(T).Name, id, _collectionName);
        var filter = Builders<T>.Filter.Eq(x => x.Id, id);
        await _collection.DeleteOneAsync(filter);
        _logger.LogInformation("Deleted document of type {@DocumentType} with id {@DocumentId} from {CollectionName}", typeof(T).Name, id, _collectionName);

        var cacheKey = $"{typeof(T).Name.ToLower()}-{id}";
        var cachedDocument = _cacheService.Get<T>(cacheKey);
        if (cachedDocument is not null)
        {
            _logger.LogInformation("Found deleted document in cache with key {@CacheKey}. Deleting cached document.", cacheKey);
            _cacheService.Remove(cacheKey);
        }
    }
}