using Kathanika.Core.Application.Services;
using MongoDB.Bson;

namespace Kathanika.Infrastructure.Persistence.FileStorage;

internal class FileMetadataService(
    ILogger<FileMetadataService> logger,
    ICacheService cacheService,
    IMongoDatabase mongoDatabase
) : IFileMetadataService
{
    private readonly IMongoCollection<StoredFileMetadata> _fileEntryCollection
        = mongoDatabase.GetCollection<StoredFileMetadata>(Constants.StoredFileMetadataCollectionName);

    private static bool IsValidFileId(string fileId)
    {
        return ObjectId.TryParse(fileId, out _);
    }

    public async Task<string> CreateAsync(string fileName, string contentType, CancellationToken cancellationToken = default)
    {
        StoredFileMetadata storedFileEntry = new(fileName, contentType, 0);
        await _fileEntryCollection.InsertOneAsync(storedFileEntry, cancellationToken: cancellationToken);

        return storedFileEntry.Id;
    }

    public async Task<bool> ExistAsync(string fileId, CancellationToken cancellationToken = default)
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

    public async Task<StoredFileMetadata?> GetAsync(string fileId, CancellationToken cancellationToken = default)
    {
        if (!IsValidFileId(fileId)) return null;
        logger.LogInformation("Getting document of type {@DocumentType} with id {@DocumentId} from {CollectionName}", typeof(StoredFileMetadata).Name, fileId, Constants.StoredFileMetadataCollectionName);

        string cacheKey = $"{typeof(StoredFileMetadata).Name.ToLower()}:{fileId}";
        logger.LogInformation("Trying to get document from cache with cache key: {@CacheKey}", cacheKey);
        StoredFileMetadata? cachedDocument = cacheService.Get<StoredFileMetadata>(cacheKey);
        if (cachedDocument is not null)
        {
            logger.LogInformation("Got document {@Document} of type {@DocumentType} from cache with cache key: {@CacheKey} ",
                cachedDocument, typeof(StoredFileMetadata).Name, cacheKey);
            return cachedDocument;
        }
        logger.LogInformation("Document not found in cache with cache key: {@CacheKey}", cacheKey);

        FilterDefinition<StoredFileMetadata> filter = Builders<StoredFileMetadata>.Filter.Eq(x => x.Id, fileId);
        IAsyncCursor<StoredFileMetadata> cursor = await _fileEntryCollection.FindAsync(filter, cancellationToken: cancellationToken);
        StoredFileMetadata metadata = await cursor.SingleOrDefaultAsync(cancellationToken: cancellationToken);
        logger.LogInformation("Got document {@Document} of type {@DocumentType} from {CollectionName}", metadata, typeof(StoredFileMetadata).Name, Constants.StoredFileMetadataCollectionName);

        logger.LogInformation("Setting document {@Document} into cache with cache key: {@CacheKey}", metadata, cacheKey);
        cacheService.Set(cacheKey, metadata);

        return metadata;
    }

    public async Task RecordFileMoveAsync(string fileId, CancellationToken cancellationToken = default)
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
            logger.LogInformation("File Move recorded {@RecordFileMoveFileId}", fileId);
            return;
        }
        logger.LogInformation("File Move record failed. {@FileMoveRecordFailedFileId}", fileId);
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
            logger.LogInformation("Upload Complete recorded {@RecordUploadCompleteFileId}", fileId);
            return;
        }
        logger.LogInformation("Upload Complete record failed. {@UploadCompleteRecordFailedFileId}", fileId);
    }

    public async Task DeleteAsync(string[] fileIds, CancellationToken cancellationToken = default)
    {
        FilterDefinition<StoredFileMetadata> filterDefinition = Builders<StoredFileMetadata>.Filter
            .In(x => x.Id, fileIds);
        DeleteResult deleteResult = await _fileEntryCollection.DeleteManyAsync(filterDefinition, cancellationToken);
        if (deleteResult.IsAcknowledged)
        {
            logger.LogInformation("Deleted {@DeletedMetadataCount} files metadata {@DeletedMetadataIds}", deleteResult.DeletedCount, fileIds);
            return;
        }
        logger.LogInformation("Deletion failed files metadata {@DeletionFailedMetadataIds}", fileIds);
    }

    public async Task<IReadOnlyList<string>> GetUnusedFileIdsAsync(CancellationToken cancellationToken = default)
    {
        DateTimeOffset timeBefore = DateTimeOffset.Now - TimeSpan.FromMinutes(10); //TODO: From appsettings...
        FilterDefinition<StoredFileMetadata> filterDefinition = Builders<StoredFileMetadata>.Filter
            .And(
                Builders<StoredFileMetadata>.Filter.Eq(x => x.IsUsed, false),
                Builders<StoredFileMetadata>.Filter.Lte(x => x.UploadCompletedAt, timeBefore)
            );
        IReadOnlyList<string> unusedFileIds = await _fileEntryCollection
            .Find(filterDefinition)
            .Project(x => x.Id)
            .ToListAsync(cancellationToken);
        return unusedFileIds;
    }
}
