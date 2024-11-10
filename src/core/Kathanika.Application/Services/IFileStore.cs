namespace Kathanika.Application.Services;

public interface IFileStore
{
    Task MoveToStoreAsync(string fileId, CancellationToken cancellationToken = default);
    Task RemoveFromStoreAsync(string fileId, CancellationToken cancellationToken = default);
    Task<(Stream stream, string contentType)> GetAsync(string fileId, CancellationToken cancellationToken = default);
    Task<bool> ValidateAsync(
        string fileId,
        long permittedMinSizeInBytes = 0,
        long permittedMaxSizeInBytes = 0,
        string[]? permittedContentTypes = null,
        string[]? permittedExtensions = null,
        CancellationToken cancellationToken = default
    );
}
