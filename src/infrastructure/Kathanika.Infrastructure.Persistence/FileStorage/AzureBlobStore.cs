using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Kathanika.Application.Services;
using Path = System.IO.Path;

namespace Kathanika.Infrastructure.Persistence.FileStorage;

internal sealed class AzureBlobStore(
    ILogger<AzureBlobStore> logger,
    BlobServiceClient blobServiceClient,
    IUploadedStore uploadedStore,
    IFileMetadataService fileMetadataService
) : FileValidator(fileMetadataService), IFileStore
{
    private const string ContainerName = "kathanika"; //TODO: from appsettings...
    private readonly IFileMetadataService _fileMetadataService = fileMetadataService;

    public async Task<(Stream stream, string contentType)> GetAsync(string fileId,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Getting file {@FileId} from azure blob storage.", fileId);
        StoredFileMetadata metadata = await _fileMetadataService.GetAsync(fileId, cancellationToken)
                                      ?? throw new ArgumentException("Invalid file ID", nameof(fileId));

        if (!metadata.IsMoved)
            return (await uploadedStore.GetFileContentAsync(fileId, cancellationToken), metadata.ContentType);

        var fileName = $"{fileId}{Path.GetExtension(metadata.FileName)}";
        BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
        BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);
        Response<BlobDownloadResult> blobDownloadResult = await blobClient.DownloadContentAsync(cancellationToken);
        return (blobDownloadResult.Value.Content.ToStream(), blobDownloadResult.Value.Details.ContentType);
    }

    public async Task MoveToStoreAsync(string fileId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Moving file {@FileId} to azure blob storage", fileId);
        await using Stream dataStream = await uploadedStore.GetFileContentAsync(fileId, cancellationToken);
        StoredFileMetadata? metadata = await _fileMetadataService.GetAsync(fileId, cancellationToken);
        if (dataStream is null || dataStream.Length == 0 || metadata is null)
            throw new FileNotFoundException();

        var fileName = $"{fileId}{Path.GetExtension(metadata.FileName)}";

        BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
        BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

        await blobClient.UploadAsync(dataStream, cancellationToken);

        await uploadedStore.DeleteFileAsync(fileId, cancellationToken);

        await _fileMetadataService.RecordFileMoveAsync(fileId, cancellationToken);
    }

    public async Task RemoveFromStoreAsync(string fileId, CancellationToken cancellationToken = default)
    {
        StoredFileMetadata? metadata = await _fileMetadataService.GetAsync(fileId, cancellationToken);
        if (metadata is null) return;

        if (!metadata.IsMoved)
        {
            await uploadedStore.DeleteFileAsync(fileId, cancellationToken);
        }
        else
        {
            var fileName = $"{fileId}{Path.GetExtension(metadata.FileName)}";
            BlobContainerClient blobContainerClient = blobServiceClient.GetBlobContainerClient(ContainerName);
            BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

            await blobClient.DeleteAsync(cancellationToken: cancellationToken);
        }

        await _fileMetadataService.DeleteAsync([fileId], cancellationToken);
    }
}