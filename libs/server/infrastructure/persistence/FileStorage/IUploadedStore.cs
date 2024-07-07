namespace Kathanika.Infrastructure.Persistence.FileStorage;

public interface IUploadedStore
{
    Task<Stream> GetFileContentAsync(string fileId, CancellationToken cancellationToken = default);
    Task DeleteFileAsync(string fileId, CancellationToken cancellationToken = default);
    Task<bool> FileExistAsync(string fileId, CancellationToken cancellationToken = default);
    Task<List<string>> GetExpiredFileIds(CancellationToken cancellationToken = default);
}
