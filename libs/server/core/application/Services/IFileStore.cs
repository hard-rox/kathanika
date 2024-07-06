namespace Kathanika.Core.Application.Services;

public interface IFileStore
{
    Task MoveToStoreAsync(string fileId, CancellationToken cancellationToken = default);
    Task RemoveFromStoreAsync(string fileId, CancellationToken cancellationToken = default);
    Task<(Stream stream, string contentType)> GetAsync(string fileId, CancellationToken cancellationToken = default);
}
