using Kathanika.Core.Application.Services;

namespace Kathanika.Infrastructure.Persistence.FileStorage;

internal sealed class DiskStorage(
    ILogger<DiskStorage> logger,
    IMongoDatabase mongoDatabase,
    IUploadedStore uploadedFileStore,
    ICacheService cacheService)
: FileMetadataService(mongoDatabase, cacheService, logger), IFileStore
{

    public async Task MoveToStoreAsync(string fileId, CancellationToken cancellationToken = default)
    {
        using Stream dataStream = await uploadedFileStore.GetFileContentAsync(fileId, cancellationToken);
        StoredFileMetadata? metadata = await GetAsync(fileId, cancellationToken);
        if (dataStream is null || dataStream.Length == 0 || metadata is null)
            throw new Exception("File not found");

        string fileName = $"{fileId}.{Path.GetExtension(metadata.FileName)}";
        string filePath = "Files"; //TODO: Move to appsettings or constants...
        if (metadata.SubDirectory is not null)
        {
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);

            filePath = Path.Combine(filePath, metadata.SubDirectory);
        }
        filePath = Path.Combine(filePath, fileName);

        using FileStream fileStream = new(filePath, FileMode.Create, FileAccess.Write);
        await dataStream.CopyToAsync(fileStream, cancellationToken);
        await uploadedFileStore.DeleteFileAsync(fileId, cancellationToken);

        await RecordFileMove(fileId, cancellationToken);
    }

    public new async Task<bool> ExistAsync(string fileId, CancellationToken cancellationToken = default)
    {
        bool hasInDiskStore = true;
        bool metadataExist = await ExistAsync(fileId, cancellationToken);

        return metadataExist && hasInDiskStore;
    }
}
