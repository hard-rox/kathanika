using Kathanika.Application.Services;
using Path = System.IO.Path;

namespace Kathanika.Infrastructure.Persistence.FileStorage;

internal sealed class DiskFileStore(
    // ILogger<DiskFileStore> logger,
    IFileMetadataService fileMetadataService,
    IUploadedStore uploadedFileStore)
    : FileValidator(fileMetadataService), IFileStore
{
    private readonly IFileMetadataService _fileMetadataService = fileMetadataService;

    public async Task<(Stream stream, string contentType)> GetAsync(string fileId,
        CancellationToken cancellationToken = default)
    {
        StoredFileMetadata? metadata = await _fileMetadataService.GetAsync(fileId, cancellationToken);
#pragma warning disable CS8619 // Nullability of reference types in value doesn't match target type.
        if (metadata is null) return (null, null); //TODO: Find better approach
#pragma warning restore CS8619 // Nullability of reference types in value doesn't match target type.

        if (!metadata.IsMoved)
            return (await uploadedFileStore.GetFileContentAsync(fileId, cancellationToken), metadata.ContentType);

        var filePath = "Files"; //TODO: Move to appSettings or constants...
        if (metadata.SubDirectory is not null) filePath = Path.Combine(filePath, metadata.SubDirectory);

        filePath = Path.Combine(filePath, $"{fileId}{Path.GetExtension(metadata.FileName)}");
        Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return (stream, metadata.ContentType);
    }

    public async Task MoveToStoreAsync(string fileId, CancellationToken cancellationToken = default)
    {
        await using Stream dataStream = await uploadedFileStore.GetFileContentAsync(fileId, cancellationToken);
        StoredFileMetadata? metadata = await _fileMetadataService.GetAsync(fileId, cancellationToken);
        if (dataStream is null || dataStream.Length == 0 || metadata is null)
            throw new FileNotFoundException();

        var fileName = $"{fileId}{Path.GetExtension(metadata.FileName)}";
        var filePath = "Files"; //TODO: Move to appSettings or constants...

        if (!Directory.Exists(filePath))
            Directory.CreateDirectory(filePath);

        if (metadata.SubDirectory is not null)
        {
            filePath = Path.Combine(filePath, metadata.SubDirectory);

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
        }

        filePath = Path.Combine(filePath, fileName);

        await using FileStream fileStream = new(filePath, FileMode.Create, FileAccess.Write);
        await dataStream.CopyToAsync(fileStream, cancellationToken);
        await uploadedFileStore.DeleteFileAsync(fileId, cancellationToken);

        await _fileMetadataService.RecordFileMoveAsync(fileId, cancellationToken);
    }

    public async Task RemoveFromStoreAsync(string fileId, CancellationToken cancellationToken = default)
    {
        StoredFileMetadata? metadata = await _fileMetadataService.GetAsync(fileId, cancellationToken);
        if (metadata is null) return;

        if (!metadata.IsMoved)
        {
            await uploadedFileStore.DeleteFileAsync(fileId, cancellationToken);
        }
        else
        {
            var fileName = $"{fileId}{Path.GetExtension(metadata.FileName)}";
            var filePath = "Files"; //TODO: Move to appsettings or constants...

            if (metadata.SubDirectory is not null) filePath = Path.Combine(filePath, metadata.SubDirectory);

            filePath = Path.Combine(filePath, fileName);
            if (File.Exists(filePath)) File.Delete(filePath);
        }

        await _fileMetadataService.DeleteAsync([fileId], cancellationToken);
    }
}