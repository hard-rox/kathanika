using MongoDB.Bson;

namespace Kathanika.Infrastructure.Persistence.FileStorage;

internal abstract class FileMetadataService(
    IMongoDatabase mongoDatabase,
    ILogger logger)
{
    private readonly IMongoCollection<StoredFileMetadata> _fileEntryCollection
        = mongoDatabase.GetCollection<StoredFileMetadata>(Constants.StoredFileMetadataCollectionName);

    private bool IsValidFileId(string fileId)
    {
        return ObjectId.TryParse(fileId, out _);
    }

    public async Task<string> CreateAsync(string fileName, string contentType, CancellationToken cancellationToken = default)
    {
        StoredFileMetadata storedFileEntry = new(fileName, contentType, 0);
        await _fileEntryCollection.InsertOneAsync(storedFileEntry, cancellationToken: cancellationToken);

        return storedFileEntry.Id;
    }

    protected async Task<bool> ExistAsync(string fileId, CancellationToken cancellationToken = default)
    {
        if (!IsValidFileId(fileId)) return false;

        FilterDefinition<StoredFileMetadata> filterDefinition = Builders<StoredFileMetadata>.Filter.Where(x => x.Id == fileId);
        CountOptions countOptions = new()
        {
            Limit = 1
        };
        long count = await _fileEntryCollection.CountDocumentsAsync(filterDefinition, countOptions, cancellationToken);
        return count > 0;
    }

    protected async Task<StoredFileMetadata?> GetAsync(string fileId, CancellationToken cancellationToken = default)
    {
        if (!IsValidFileId(fileId)) return null;
        logger.LogInformation("Getting document of type {@DocumentType} with id {@DocumentId} from {CollectionName}", typeof(StoredFileMetadata).Name, fileId, Constants.StoredFileMetadataCollectionName);

        string cacheKey = $"{typeof(StoredFileMetadata).Name.ToLower()}:{fileId}";
        logger.LogInformation("Trying to get document from cache with cache key: {@CacheKey}", cacheKey);
        // StoredFileMetadata? cachedDocument = cacheService.Get<StoredFileMetadata>(cacheKey);
        // if (cachedDocument is not null)
        // {
        //     logger.LogInformation("Got document {@Document} of type {@DocumentType} from cache with cache key: {@CacheKey} ",
        //         cachedDocument, typeof(StoredFileMetadata).Name, cacheKey);
        //     return cachedDocument;
        // }
        logger.LogInformation("Document not found in cache with cache key: {@CacheKey}", cacheKey);

        FilterDefinition<StoredFileMetadata> filter = Builders<StoredFileMetadata>.Filter.Eq(x => x.Id, fileId);
        IAsyncCursor<StoredFileMetadata> cursor = await _fileEntryCollection.FindAsync(filter, cancellationToken: cancellationToken);
        StoredFileMetadata metadata = await cursor.SingleOrDefaultAsync(cancellationToken: cancellationToken);
        logger.LogInformation("Got document {@Document} of type {@DocumentType} from {CollectionName}", metadata, typeof(StoredFileMetadata).Name, Constants.StoredFileMetadataCollectionName);

        logger.LogInformation("Setting document {@Document} into cache with cache key: {@CacheKey}", metadata, cacheKey);
        // cacheService.Set(cacheKey, metadata);

        return metadata;
    }

    protected async Task RecordFileMove(string fileId, CancellationToken cancellationToken = default)
    {
        FilterDefinition<StoredFileMetadata> filterDefinition = Builders<StoredFileMetadata>.Filter
            .Eq(x => x.Id, fileId);
        UpdateDefinition<StoredFileMetadata> updateDefinition = Builders<StoredFileMetadata>
            .Update
            .Set(x => x.IsUsed, true)
            .Set(x => x.IsMoved, true)
            .Set(x => x.LastMovedAt, DateTimeOffset.Now);

        UpdateResult result = await _fileEntryCollection.UpdateOneAsync(filterDefinition, updateDefinition, cancellationToken: cancellationToken);
        if (!result.IsAcknowledged)
        {
            //TODO: Log...
        }
    }

    public async Task RecordUploadCompletedAsync(string fileId, long fileSizeInBytes, CancellationToken cancellationToken = default)
    {
        FilterDefinition<StoredFileMetadata> filterDefinition = Builders<StoredFileMetadata>.Filter
            .Eq(x => x.Id, fileId);
        UpdateDefinition<StoredFileMetadata> updateDefinition = Builders<StoredFileMetadata>
            .Update
            .Set(x => x.SizeInBytes, fileSizeInBytes)
            .Set(x => x.UploadCompletedAt, DateTimeOffset.Now);
        UpdateResult result = await _fileEntryCollection.UpdateOneAsync(filterDefinition, updateDefinition, cancellationToken: cancellationToken);
        if (!result.IsAcknowledged)
        {
            //TODO: Log...
        }
    }
}
