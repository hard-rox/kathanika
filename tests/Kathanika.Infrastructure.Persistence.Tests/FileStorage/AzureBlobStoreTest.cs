using Azure.Storage.Blobs;
using Kathanika.Infrastructure.Persistence.FileStorage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute.ReturnsExtensions;

namespace Kathanika.Infrastructure.Persistence.Tests.FileStorage;

public sealed class AzureBlobStoreTest
{
    private readonly ILogger<AzureBlobStore> _nullLogger = new NullLogger<AzureBlobStore>();
    private readonly BlobServiceClient _blobServiceClient = Substitute.For<BlobServiceClient>();
    private readonly IUploadedStore _uploadedStore = Substitute.For<IUploadedStore>();
    private readonly IFileMetadataService _fileMetadataService = Substitute.For<IFileMetadataService>();

    [Fact]
    public async Task GetAsync_ShouldThrowException_WhenMetadataNotFound()
    {
        _fileMetadataService.GetAsync(Arg.Any<string>())
            .ReturnsNull();
        AzureBlobStore azureBlobStore = new(_nullLogger, _blobServiceClient, _uploadedStore, _fileMetadataService);

        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() => azureBlobStore.GetAsync("234"));

        Assert.Equal("fileId", exception.ParamName);
    }

    [Fact]
    public async Task GetAsync_ShouldGetFromStore_WhenFileIsNotMoved()
    {
        _fileMetadataService.GetAsync(Arg.Is<string>(x => x == "234"))
            .Returns(new StoredFileMetadata("dummy-file.tst", "file/tst", 123));
        AzureBlobStore azureBlobStore = new(_nullLogger, _blobServiceClient, _uploadedStore, _fileMetadataService);

        _ = azureBlobStore.GetAsync("234");

        await _uploadedStore.Received(1).GetFileContentAsync(Arg.Is<string>(x => x == "234"));
    }
}