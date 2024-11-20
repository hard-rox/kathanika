namespace Kathanika.Infrastructure.Persistence.FileStorage;

public interface IFileMetadataService
{
    Task<string> CreateAsync(string fileName, string contentType, CancellationToken cancellationToken = default);
    Task DeleteAsync(string?[] fileIds, CancellationToken cancellationToken = default);
    Task<bool> ExistAsync(string fileId, CancellationToken cancellationToken = default);
    internal Task<StoredFileMetadata?> GetAsync(string fileId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<string>> GetUnusedFileIdsAsync(CancellationToken cancellationToken = default);
    internal Task RecordFileMoveAsync(string fileId, CancellationToken cancellationToken = default);
    Task RecordUploadCompletedAsync(string fileId, long fileSizeInBytes, CancellationToken cancellationToken = default);
}