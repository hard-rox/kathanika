using System.Linq.Expressions;
using System.Reflection;
using Kathanika.Application.Services;
using Kathanika.Domain.Primitives;
using Kathanika.Infrastructure.Persistence.Outbox;
using MongoDB.Bson;

namespace Kathanika.Infrastructure.Persistence;

internal abstract class Repository<T> : IRepository<T> where T : AggregateRoot
{
    private readonly ICacheService _cacheService;
    private readonly IMongoCollection<T> _collection;
    private readonly string _collectionName;
    private readonly ILogger<IRepository<T>> _logger;
    private readonly IMongoCollection<OutboxMessage> _outboxMessageCollection;

    protected Repository(IMongoDatabase database, string collectionName, ILogger<IRepository<T>> logger,
        ICacheService cacheService)
    {
        _collectionName = collectionName.ToLower();
        _outboxMessageCollection = database.GetCollection<OutboxMessage>(Constants.OutboxMessageCollectionName);
        _collection = database.GetCollection<T>(_collectionName);
        _logger = logger;
        _cacheService = cacheService;
    }

    public IQueryable<T> AsQueryable()
    {
        return _collection.AsQueryable();
    }

    public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
    {
        if (!IsValidId(id)) return false;
        FilterDefinition<T> filterDefinition = Builders<T>.Filter.Where(x => x.Id == id);
        CountOptions countOptions = new()
        {
            Limit = 1
        };
        var count = await _collection.CountDocumentsAsync(filterDefinition, countOptions, cancellationToken);
        return count > 0;
    }

    public async Task<bool> ExistsAsync(Expression<Func<T, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        FilterDefinition<T> filterDefinition = Builders<T>.Filter.Where(expression);
        CountOptions countOptions = new()
        {
            Limit = 1
        };
        var count = await _collection.CountDocumentsAsync(filterDefinition, countOptions, cancellationToken);
        return count > 0;
    }

    public async Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        if (!IsValidId(id)) return null;
        _logger.LogInformation("Getting document of type {@DocumentType} with id {@DocumentId} from {CollectionName}",
            typeof(T).Name, id, _collectionName);

        var cacheKey = $"{typeof(T).Name.ToLower()}:{id}";
        _logger.LogInformation("Trying to get document from cache with cache key: {@CacheKey}", cacheKey);
        T? cachedDocument = _cacheService.Get<T>(cacheKey);
        if (cachedDocument is not null)
        {
            _logger.LogInformation(
                "Got document {@Document} of type {@DocumentType} from cache with cache key: {@CacheKey} ",
                cachedDocument, typeof(T).Name, cacheKey);
            return cachedDocument;
        }

        _logger.LogInformation("Document not found in cache with cache key: {@CacheKey}", cacheKey);

        FilterDefinition<T> filter = Builders<T>.Filter.Where(x => x.Id == id);
        IAsyncCursor<T> cursor = await _collection.FindAsync(filter, cancellationToken: cancellationToken);
        T document = await cursor.SingleOrDefaultAsync(cancellationToken);
        _logger.LogInformation("Got document {@Document} of type {@DocumentType} from {CollectionName}", document,
            typeof(T).Name, _collectionName);

        _logger.LogInformation("Setting document {@Document} into cache with cache key: {@CacheKey}", document,
            cacheKey);
        _cacheService.Set(cacheKey, document);

        return document;
    }

    public async Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting all documents of type {@DocumentType} from collection {@CollectionName}",
            typeof(T).Name, _collectionName);
        FilterDefinition<T> filter = Builders<T>.Filter.Empty;
        IAsyncCursor<T> cursor = await _collection.FindAsync(filter, cancellationToken: cancellationToken);
        return await cursor.ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<T>> ListAllAsync(Expression<Func<T, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Getting all documents satisfying condition {@Condition} of type {@DocumentType} from collection {@CollectionName}",
            expression.ToJson(),
            typeof(T).Name,
            _collectionName);
        FilterDefinition<T> filter = Builders<T>.Filter.Where(expression);
        IAsyncCursor<T> cursor = await _collection.FindAsync(filter, cancellationToken: cancellationToken);
        return await cursor.ToListAsync(cancellationToken);
    }

    public async Task<long> CountAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Getting document count of all documents of type {@DocumentType} from collection {@CollectionName}",
            typeof(T).Name, _collectionName);
        var cacheKey = $"{typeof(T).Name.ToLower()}:count";
        var cachedDocumentCount = _cacheService.Get<long?>(cacheKey);
        if (cachedDocumentCount is not null)
        {
            _logger.LogInformation(
                "Got document count {@DocumentCount} of type {@DocumentType} from cache with cache key: {@CacheKey} ",
                cachedDocumentCount, typeof(T).Name, cacheKey);
            return (long)cachedDocumentCount;
        }

        _logger.LogInformation("Document count not found in cache with cache key: {@CacheKey}", cacheKey);

        FilterDefinition<T> filter = Builders<T>.Filter.Empty;
        var documentCount = await _collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);

        _logger.LogInformation("Got document count {@DocumentCount} of type {@DocumentType} from {CollectionName}",
            documentCount, typeof(T).Name, _collectionName);

        _logger.LogInformation("Setting document count {@DocumentCount} into cache with cache key: {@CacheKey}",
            documentCount, cacheKey);
        _cacheService.Set(cacheKey, documentCount);
        return documentCount;
    }

    public async Task<long> CountAsync(Expression<Func<T, bool>> expression,
        CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Getting document count in condition {@Condition} of type {@DocumentType} from collection {@CollectionName}",
            expression, typeof(T).Name, _collectionName);
        FilterDefinition<T> filter = Builders<T>.Filter.Where(expression);
        var documentCount = await _collection.CountDocumentsAsync(filter, cancellationToken: cancellationToken);

        _logger.LogInformation(
            "Got document count {@DocumentCount} in condition {@Condition} of type {@DocumentType} from {CollectionName}",
            documentCount, expression, typeof(T).Name, _collectionName);

        return documentCount;
    }

    public async Task<T> AddAsync(T aggregate, CancellationToken cancellationToken = default)
    {
        SetCreationAuditProperties(aggregate);
        _logger.LogInformation(
            "Adding new document {@Document} of type {@DocumentType} into collection {@CollectionName}", aggregate,
            typeof(T).Name, _collectionName);
        await _collection.InsertOneAsync(aggregate, cancellationToken: cancellationToken);
        _logger.LogInformation(
            "Added new document with _id {@_id} of type {@DocumentType} into collection {@CollectionName}",
            aggregate.ToBsonDocument()["_id"].ToJson(), typeof(T).Name, _collectionName);

        List<OutboxMessage> outboxMessages = GetOutboxMessagesFromAggregate(aggregate);
        if (outboxMessages.Count > 0)
            await _outboxMessageCollection.InsertManyAsync(outboxMessages, cancellationToken: cancellationToken);

        return aggregate;
    }

    public async Task UpdateAsync(T aggregate, CancellationToken cancellationToken = default)
    {
        SetModificationAuditProperties(aggregate);
        _logger.LogInformation(
            "Updating document of type {@DocumentType} with id {@DocumentId} from {CollectionName} with value {@NewValue}",
            typeof(T).Name,
            aggregate.Id,
            _collectionName,
            aggregate);

        FilterDefinition<T> filter = Builders<T>.Filter.Eq(x => x.Id, aggregate.Id);
        await _collection.ReplaceOneAsync(filter, aggregate, cancellationToken: cancellationToken);
        _logger.LogInformation(
            "Updated document of type {@DocumentType} with id {@DocumentId} from {CollectionName} with value {@NewValue}",
            typeof(T).Name, aggregate.Id, _collectionName, aggregate);

        List<OutboxMessage> outboxMessages = GetOutboxMessagesFromAggregate(aggregate);
        if (outboxMessages.Count > 0)
            await _outboxMessageCollection.InsertManyAsync(outboxMessages, cancellationToken: cancellationToken);

        var cacheKey = $"{typeof(T).Name.ToLower()}:{aggregate.Id}";
        T? cachedDocument = _cacheService.Get<T>(cacheKey);
        if (cachedDocument is not null)
        {
            _logger.LogInformation("Found updating document in cache with key {@CacheKey}. Updating cached document.",
                cacheKey);
            _cacheService.Set(cacheKey, aggregate);
        }
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        // if (!IsValidId(id)) throw new NotFoundWithTheIdException(typeof(T), id);
        _logger.LogInformation("Deleting document of type {@DocumentType} with id {@DocumentId} from {CollectionName}",
            typeof(T).Name, id, _collectionName);
        FilterDefinition<T> filter = Builders<T>.Filter.Eq(x => x.Id, id);
        await _collection.DeleteOneAsync(filter, cancellationToken);
        _logger.LogInformation("Deleted document of type {@DocumentType} with id {@DocumentId} from {CollectionName}",
            typeof(T).Name, id, _collectionName);

        var cacheKey = $"{typeof(T).Name.ToLower()}:{id}";
        T? cachedDocument = _cacheService.Get<T>(cacheKey);
        if (cachedDocument is not null)
        {
            _logger.LogInformation("Found deleted document in cache with key {@CacheKey}. Deleting cached document.",
                cacheKey);
            _cacheService.Remove(cacheKey);
        }
    }

    private static bool IsValidId(string id)
    {
        return ObjectId.TryParse(id, out ObjectId _);
    }

    private static List<OutboxMessage> GetOutboxMessagesFromAggregate(T aggregate)
    {
        List<OutboxMessage> outboxMessages = aggregate.DomainEvents
            .Select(domainEvent => new OutboxMessage(domainEvent))
            .ToList();
        aggregate.ClearDomainEvents();
        return outboxMessages;
    }

    private static void SetCreationAuditProperties(T aggregate)
    {
        Type aggregateType = typeof(AggregateRoot);
        PropertyInfo? createdAtProperty = aggregateType.GetProperty(nameof(aggregate.CreatedAt));
        createdAtProperty?.SetValue(aggregate, DateTimeOffset.Now);
        PropertyInfo? createdByUserIdProperty = aggregateType.GetProperty(nameof(aggregate.CreatedByUserId));
        createdByUserIdProperty?.SetValue(aggregate, "not implemented");
        PropertyInfo? createdByUserNameProperty = aggregateType.GetProperty(nameof(aggregate.CreatedByUserName));
        createdByUserNameProperty?.SetValue(aggregate, "not implemented");
    }

    private static void SetModificationAuditProperties(T aggregate)
    {
        Type aggregateType = typeof(AggregateRoot);
        PropertyInfo? lastModifiedAtProperty = aggregateType.GetProperty(nameof(aggregate.LastModifiedAt));
        lastModifiedAtProperty?.SetValue(aggregate, DateTimeOffset.Now);
        PropertyInfo? lastModifiedByUserIdProperty = aggregateType.GetProperty(nameof(aggregate.LastModifiedByUserId));
        lastModifiedByUserIdProperty?.SetValue(aggregate, "not implemented");
        PropertyInfo? lastModifiedByUserNameProperty =
            aggregateType.GetProperty(nameof(aggregate.LastModifiedByUserName));
        lastModifiedByUserNameProperty?.SetValue(aggregate, "not implemented");
    }
}