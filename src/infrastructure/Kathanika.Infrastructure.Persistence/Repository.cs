using System.Linq.Expressions;
using System.Reflection;
using Kathanika.Domain.Primitives;
using Kathanika.Infrastructure.Persistence.Outbox;
using Microsoft.Extensions.Caching.Hybrid;
using MongoDB.Bson;

namespace Kathanika.Infrastructure.Persistence;

/// <summary>
/// Represents a generic repository that provides basic data access functionality for an entity of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">
/// The type of the entity that this repository will manage. Must inherit from <see cref="AggregateRoot"/>.
/// </typeparam>
/// <remarks>
/// This abstract class implements the <see cref="IRepository{T}"/> interface and provides commonly used operations
/// such as querying, checking existence, retrieving, adding, updating, deleting, and counting entities in a given MongoDB collection.
/// </remarks>
/// <example>
/// This class is intended for use as a base class for specific repositories in your system.
/// Examples include repositories for vendors, bibliographic records, patrons, and purchase orders.
/// </example>
internal abstract class Repository<T> : IRepository<T> where T : AggregateRoot
{
    private readonly HybridCache _hybridCache;
    private readonly IMongoCollection<T> _collection;
    private readonly string _collectionName;
    private readonly ILogger<Repository<T>> _logger;
    private readonly IMongoCollection<OutboxMessage> _outboxMessageCollection;

    protected Repository(IMongoDatabase database, string collectionName, ILogger<Repository<T>> logger, HybridCache hybridCache)
    {
        _collectionName = collectionName;
        _outboxMessageCollection = database.GetCollection<OutboxMessage>(Constants.OutboxMessageCollectionName);
        _collection = database.GetCollection<T>(_collectionName);
        _logger = logger;
        _hybridCache = hybridCache;
    }

    private static bool IsValidId(string id)
    {
        return Ulid.TryParse(id, out Ulid _);
    }

    private static List<OutboxMessage> GetOutboxMessagesFromAggregate(T aggregate)
    {
        List<OutboxMessage> outboxMessages = aggregate.DomainEvents
            .Select(domainEvent => new OutboxMessage(domainEvent))
            .ToList();
        return outboxMessages;
    }

    private async Task<T> GetDocumentFromDb(string id, CancellationToken cancellationToken)
    {
        FilterDefinition<T> filter = Builders<T>.Filter.Where(x => x.Id == id);
        IAsyncCursor<T> cursor = await _collection.FindAsync(filter, cancellationToken: cancellationToken);
        T document = await cursor.SingleOrDefaultAsync(cancellationToken);
        _logger.LogInformation("Got document {@Document} of type {@DocumentType} from {CollectionName}", document,
            typeof(T).Name, _collectionName);
        return document;
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

    /// <summary>
    /// Provides a LINQ queryable interface for entities of type <typeparamref name="T"/> within the MongoDB collection.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the entity that the repository manages. Must inherit from <see cref="AggregateRoot"/>.
    /// </typeparam>
    /// <returns>
    /// An <see cref="IQueryable{T}"/> that allows LINQ queries to be performed on the entities in the collection.
    /// </returns>
    public IQueryable<T> AsQueryable()
    {
        return _collection.AsQueryable();
    }

    /// <summary>
    /// Asynchronously checks whether an entity with the specified ID exists in the repository.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the entity to check for existence.
    /// </param>
    /// <param name="cancellationToken">
    /// An optional <see cref="CancellationToken"/> to observe while waiting for the task to complete.
    /// </param>
    /// <returns>
    /// A <see cref="Task{Boolean}"/> representing the asynchronous operation. The task result is <c>true</c> if an entity with the given ID exists in the repository; otherwise, <c>false</c>.
    /// </returns>
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

    /// <summary>
    /// Asynchronously checks whether any entities in the MongoDB collection satisfy the specified predicate.
    /// </summary>
    /// <param name="expression">
    /// A LINQ expression to define the condition that entities should fulfill to be considered as existing.
    /// </param>
    /// <param name="cancellationToken">
    /// Optional. A token to observe while waiting for the task to complete.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation. The task result contains a boolean value indicating whether
    /// any entity satisfies the specified condition.
    /// </returns>
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

    /// <summary>
    /// Retrieves an entity of type <typeparamref name="T"/> by its unique identifier asynchronously.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the entity to retrieve.
    /// </param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken"/> to observe while waiting for the task to complete.
    /// </param>
    /// <typeparam name="T">
    /// The type of the entity that the repository manages. Must inherit from <see cref="AggregateRoot"/>.
    /// </typeparam>
    /// <returns>
    /// An entity of type <typeparamref name="T"/> if found; otherwise, <c>null</c>.
    /// </returns>
    public async Task<T?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        if (!IsValidId(id)) return null;
        _logger.LogInformation("Getting document of type {@DocumentType} with id {@DocumentId} from {CollectionName}",
            typeof(T).Name, id, _collectionName);

        var cacheKey = $"{typeof(T).Name.ToLower()}{id}";
        T document = await _hybridCache.GetOrCreateAsync<T>(
            cacheKey,
            async token => await GetDocumentFromDb(id, token),
            cancellationToken: cancellationToken);

        return document;
    }

    /// <summary>
    /// Retrieves all entities of type <typeparamref name="T"/> from the MongoDB collection.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the entity that the repository manages. Must inherit from <see cref="AggregateRoot"/>.
    /// </typeparam>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used to cancel the asynchronous operation.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation. The task's result contains a read-only list of entities of type <typeparamref name="T"/> retrieved from the collection.
    /// </returns>
    public async Task<IReadOnlyList<T>> ListAllAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Getting all documents of type {@DocumentType} from collection {@CollectionName}",
            typeof(T).Name, _collectionName);
        FilterDefinition<T> filter = Builders<T>.Filter.Empty;
        IAsyncCursor<T> cursor = await _collection.FindAsync(filter, cancellationToken: cancellationToken);
        return await cursor.ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Retrieves a read-only list of entities of type <typeparamref name="T"/> from the repository that satisfy the specified condition.
    /// </summary>
    /// <param name="expression">
    /// A LINQ expression to define the condition that the entities must satisfy.
    /// </param>
    /// <param name="cancellationToken">
    /// A token to monitor for cancellation requests.
    /// </param>
    /// <typeparam name="T">
    /// The type of the entity that the repository manages. Must inherit from <see cref="AggregateRoot"/>.
    /// </typeparam>
    /// <returns>
    /// A task that represents the asynchronous operation. The task result contains a read-only list of entities of type <typeparamref name="T"/>.
    /// </returns>
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

    /// <summary>
    /// Asynchronously retrieves the current count of documents of type <typeparamref name="T"/> in the corresponding MongoDB collection.
    /// </summary>
    /// <param name="cancellationToken">
    /// A token that allows the asynchronous operation to be canceled.
    /// </param>
    /// <returns>
    /// A task that represents the asynchronous operation, containing the count of documents as a <see cref="long"/>.
    /// </returns>
    public async Task<long> CountAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation(
            "Getting document count of all documents of type {@DocumentType} from collection {@CollectionName}",
            typeof(T).Name, _collectionName);
        var cacheKey = $"{typeof(T).Name.ToLower()}-count";

        FilterDefinition<T> filter = Builders<T>.Filter.Empty;
        var documentCount = await _hybridCache.GetOrCreateAsync(
            cacheKey,
            async token => await _collection.CountDocumentsAsync(filter, cancellationToken: token),
            cancellationToken: cancellationToken);
        _logger.LogInformation("Got document count {@DocumentCount} of type {@DocumentType} from {CollectionName}",
            documentCount, typeof(T).Name, _collectionName);

        return documentCount;
    }

    /// <summary>
    /// Asynchronously counts the number of documents of type <typeparamref name="T"/> in the MongoDB collection
    /// that satisfy the specified filter expression.
    /// </summary>
    /// <param name="expression">
    /// An expression specifying the condition that the documents must satisfy to be included in the count.
    /// </param>
    /// <param name="cancellationToken">
    /// An optional token to cancel the asynchronous operation.
    /// </param>
    /// <returns>
    /// A <see cref="Task{TResult}"/> representing the asynchronous operation, with the result being the count of documents that match the condition.
    /// </returns>
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

    /// <summary>
    /// Adds a new aggregate of type <typeparamref name="T"/> to the corresponding repository and persists it.
    /// </summary>
    /// <param name="aggregate">
    /// The aggregate instance to be added. Must inherit from <see cref="AggregateRoot"/>.
    /// </param>
    /// <param name="cancellationToken">
    /// A <see cref="CancellationToken"/> that can be used to cancel the operation. Defaults to <see cref="CancellationToken.None"/>.
    /// </param>
    /// <returns>
    /// The added aggregate of type <typeparamref name="T"/> with updated state.
    /// </returns>
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

        var cacheKey = $"{typeof(T).Name.ToLower()}-{aggregate.Id}";
        await _hybridCache.SetAsync(cacheKey, aggregate, cancellationToken: cancellationToken);

        List<OutboxMessage> outboxMessages = GetOutboxMessagesFromAggregate(aggregate);
        aggregate.ClearDomainEvents();
        if (outboxMessages.Count > 0)
            await _outboxMessageCollection.InsertManyAsync(outboxMessages, cancellationToken: cancellationToken);

        return aggregate;
    }

    /// <summary>
    /// Updates an existing entity of type <typeparamref name="T"/> in the repository and persists the changes to the MongoDB collection.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the entity that the repository manages. Must inherit from <see cref="AggregateRoot"/>.
    /// </typeparam>
    /// <param name="aggregate">
    /// The aggregate to be updated in the database. Must be an instance of <typeparamref name="T"/>.
    /// </param>
    /// <param name="cancellationToken">
    /// The cancellation token that can be used to cancel the operation. Defaults to <see cref="CancellationToken.None"/>.
    /// </param>
    /// <returns>
    /// A <see cref="Task"/> that represents the asynchronous operation of updating the entity.
    /// </returns>
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
        aggregate.ClearDomainEvents();
        if (outboxMessages.Count > 0)
            await _outboxMessageCollection.InsertManyAsync(outboxMessages, cancellationToken: cancellationToken);

        var cacheKey = $"{typeof(T).Name.ToLower()}-{aggregate.Id}";
        await _hybridCache.SetAsync(cacheKey, aggregate, cancellationToken: cancellationToken);
    }

    /// <summary>
    /// Deletes a document with the specified identifier from the collection and removes its associated cache entry.
    /// </summary>
    /// <param name="id">
    /// The unique identifier of the document to be deleted.
    /// </param>
    /// <param name="cancellationToken">
    /// A cancellation token that can be used to cancel the operation.
    /// </param>
    /// <returns>
    /// A task representing the asynchronous operation.
    /// </returns>
    public async Task DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Deleting document of type {@DocumentType} with id {@DocumentId} from {CollectionName}",
            typeof(T).Name, id, _collectionName);
        FilterDefinition<T> filter = Builders<T>.Filter.Eq(x => x.Id, id);
        await _collection.DeleteOneAsync(filter, cancellationToken);
        _logger.LogInformation("Deleted document of type {@DocumentType} with id {@DocumentId} from {CollectionName}",
            typeof(T).Name, id, _collectionName);

        var cacheKey = $"{typeof(T).Name.ToLower()}-{id}";
        await _hybridCache.RemoveAsync(cacheKey, cancellationToken);
    }
}