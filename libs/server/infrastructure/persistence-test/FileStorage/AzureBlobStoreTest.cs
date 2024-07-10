using System.Reflection;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Kathanika.Infrastructure.Persistence.FileStorage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute.ReturnsExtensions;

namespace Kathanika.Infrastructure.Persistence.Test.FileStorage;

public sealed class AzureBlobStoreTest
{
    private readonly ILogger<AzureBlobStore> nullLogger = new NullLogger<AzureBlobStore>();
    private readonly BlobServiceClient blobServiceClient = Substitute.For<BlobServiceClient>();
    private readonly IUploadedStore uploadedStore = Substitute.For<IUploadedStore>();
    private readonly IFileMetadataService fileMetadataService = Substitute.For<IFileMetadataService>();

    [Fact]
    public async Task GetAsync_ShouldThrowException_WhenMetadataNotFound()
    {
        fileMetadataService.GetAsync(Arg.Any<string>())
            .ReturnsNull();
        AzureBlobStore azureBlobStore = new(nullLogger, blobServiceClient, uploadedStore, fileMetadataService);

        ArgumentException exception = await Assert.ThrowsAsync<ArgumentException>(() => azureBlobStore.GetAsync("234"));

        Assert.Equal("fileId", exception.ParamName);
    }

    [Fact]
    public async Task GetAsync_ShouldGetFromStore_WhenFileIsNotMoved()
    {
        fileMetadataService.GetAsync(Arg.Is<string>(x => x == "234"))
            .Returns(new StoredFileMetadata("dummy-file.tst", "file/tst", 123));
        AzureBlobStore azureBlobStore = new(nullLogger, blobServiceClient, uploadedStore, fileMetadataService);

        _ = azureBlobStore.GetAsync("234");

        await uploadedStore.Received(1).GetFileContentAsync(Arg.Is<string>(x => x == "234"), default);
    }
}
