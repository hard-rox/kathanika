namespace Kathanika.Infrastructure.Persistence.FileStorage;

public interface IFileMetadataService
{
    Task<string> CreateAsync(string fileName, string contentType, CancellationToken cancellationToken = default);
    Task<bool> ExistAsync(string fileId, CancellationToken cancellationToken = default);
    Task RecordUploadCompletedAsync(string fileId, long fileSizeInBytes, CancellationToken cancellationToken = default);
    internal Task<StoredFileMetadata?> GetAsync(string fileId, CancellationToken cancellationToken = default);
    internal Task RecordFileMove(string fileId, CancellationToken cancellationToken = default);
}
