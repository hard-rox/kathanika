using Kathanika.Core.Application.Services;

namespace Kathanika.Infrastructure.Persistence.FileStorage;

internal sealed class DiskFileStore(
    ILogger<DiskFileStore> logger,
    IFileMetadataService fileMetadataService,
    IUploadedStore uploadedFileStore)
: IFileStore
{
    public async Task<(Stream stream, string contentType)> GetAsync(string fileId, CancellationToken cancellationToken = default)
    {
        StoredFileMetadata? metadata = await fileMetadataService.GetAsync(fileId, cancellationToken);
        if (metadata is null) return (null, null);

        if (metadata.IsMoved)
        {
            string filePath = "Files"; //TODO: Move to appsettings or constants...
            if (metadata.SubDirectory is not null) filePath = Path.Combine(filePath, metadata.SubDirectory);

            filePath = Path.Combine(filePath, $"{fileId}{Path.GetExtension(metadata.FileName)}");
            Stream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            return (stream, metadata.ContentType);
        }

        return (await uploadedFileStore.GetFileContentAsync(fileId, cancellationToken), metadata.ContentType);
    }

    public async Task MoveToStoreAsync(string fileId, CancellationToken cancellationToken = default)
    {
        using Stream dataStream = await uploadedFileStore.GetFileContentAsync(fileId, cancellationToken);
        StoredFileMetadata? metadata = await fileMetadataService.GetAsync(fileId, cancellationToken);
        if (dataStream is null || dataStream.Length == 0 || metadata is null)
            throw new Exception("File not found");

        string fileName = $"{fileId}{Path.GetExtension(metadata.FileName)}";
        string filePath = "Files"; //TODO: Move to appsettings or constants...

        if (!Directory.Exists(filePath))
            Directory.CreateDirectory(filePath);

        if (metadata.SubDirectory is not null)
        {
            filePath = Path.Combine(filePath, metadata.SubDirectory);

            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
        }
        filePath = Path.Combine(filePath, fileName);

        using FileStream fileStream = new(filePath, FileMode.Create, FileAccess.Write);
        await dataStream.CopyToAsync(fileStream, cancellationToken);
        await uploadedFileStore.DeleteFileAsync(fileId, cancellationToken);

        await fileMetadataService.RecordFileMove(fileId, cancellationToken);
    }

    public async Task RemoveFromStoreAsync(string fileId, CancellationToken cancellationToken = default)
    {
        await uploadedFileStore.DeleteFileAsync(fileId, cancellationToken);
    }
}
