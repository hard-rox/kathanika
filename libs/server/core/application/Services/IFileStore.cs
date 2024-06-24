namespace Kathanika.Core.Application.Services;

public interface IFileStore
{
    Task<string> CreateAsync(string fileName, string contentType, CancellationToken cancellationToken = default);
    Task RecordUploadCompletedAsync(string fileId, long fileSizeInBytes, CancellationToken cancellationToken = default);
    Task<bool> ExistAsync(string fileId, CancellationToken cancellationToken = default);
    Task MoveToStoreAsync(string fileId, CancellationToken cancellationToken = default);
}
