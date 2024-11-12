using Kathanika.Infrastructure.Persistence.FileStorage;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Kathanika.Infrastructure.Persistence.Tests.FileStorage;

public sealed class FileMetadataServiceTests
{
    private readonly ILogger<FileMetadataService> _logger = Substitute.For<ILogger<FileMetadataService>>();
    // private readonly ICacheService cacheService = Substitute.For<ICacheService>();
    private readonly IMongoDatabase _mongoDatabase = Substitute.For<IMongoDatabase>();
    private readonly IMongoCollection<StoredFileMetadata> _collection = Substitute.For<IMongoCollection<StoredFileMetadata>>();
    private readonly FileMetadataService _fileMetadataService;

    public FileMetadataServiceTests()
    {
        _mongoDatabase.GetCollection<StoredFileMetadata>(Arg.Any<string>()).Returns(_collection);
        _fileMetadataService = new(_logger, _mongoDatabase);
    }

    [Fact]
    public async Task CreateAsync_ShouldCallInsertAsync_WithValidData()
    {
        _ = await _fileMetadataService.CreateAsync("filename.tst", "text/plain", CancellationToken.None);

        await _collection.Received(1).InsertOneAsync(Arg.Is<StoredFileMetadata>(x => x.FileName == "filename.tst"));
    }

    [Fact]
    public async Task ExistAsync_ShouldReturnFalse_WhenFileIdInvalid()
    {
        bool exists = await _fileMetadataService.ExistAsync("testId");

        Assert.False(exists);
    }

    [Fact]
    public async Task ExistAsync_ShouldLimitCountToOne_WhenCalled()
    {
        _ = await _fileMetadataService.ExistAsync(ObjectId.GenerateNewId().ToString());

        await _collection.Received(1)
            .CountDocumentsAsync(Arg.Any<FilterDefinition<StoredFileMetadata>>(),
                Arg.Is<CountOptions>(x => x.Limit == 1));
    }

    [Fact]
    public async Task ExistAsync_ShouldReturnTrue_WhenFileIdValid()
    {
        string fileId = ObjectId.GenerateNewId().ToString();
        _collection.CountDocumentsAsync(Arg.Any<FilterDefinition<StoredFileMetadata>>(),
                Arg.Is<CountOptions>(x => x.Limit == 1))
                .Returns(1);

        bool exist = await _fileMetadataService.ExistAsync(fileId);

        Assert.True(exist);
    }

    // [Fact]
    // public async Task GetAsync_ShouldReturnFromCache_WhenFoundInCache()
    // {
    //     string fileId = ObjectId.GenerateNewId().ToString();
    //     StoredFileMetadata metadata = new(fileId, string.Empty, 0);
    //     cacheService.Get<StoredFileMetadata>(Arg.Any<string>()).Returns(metadata);

    //     StoredFileMetadata? result = await fileMetadataService.GetAsync(fileId);

    //     Assert.NotNull(result);
    //     Assert.Equal(metadata.Id, result?.Id);
    //     cacheService.Received(1).Get<StoredFileMetadata>(Arg.Any<string>());
    // }

    [Fact]
    public async Task RecordFileMoveAsync_ShouldUpdateMetadata_WhenCalled()
    {
        string fileId = Guid.NewGuid().ToString();

        await _fileMetadataService.RecordFileMoveAsync(fileId);

        await _collection.Received(1)
            .UpdateOneAsync(
                Arg.Any<FilterDefinition<StoredFileMetadata>>(),
                Arg.Any<UpdateDefinition<StoredFileMetadata>>(),
                cancellationToken: Arg.Any<CancellationToken>());
    }
}