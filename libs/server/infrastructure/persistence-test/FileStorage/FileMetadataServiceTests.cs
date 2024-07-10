using Kathanika.Core.Application.Services;
using Kathanika.Infrastructure.Persistence.FileStorage;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Kathanika.Infrastructure.Persistence.Test.FileStorage;

public sealed class FileMetadataServiceTests
{
    private readonly ILogger<FileMetadataService> nullLogger = new NullLogger<FileMetadataService>();
    private readonly ICacheService cacheService = Substitute.For<ICacheService>();
    private readonly IMongoDatabase mongoDatabase = Substitute.For<IMongoDatabase>();
    private readonly IMongoCollection<StoredFileMetadata> collection = Substitute.For<IMongoCollection<StoredFileMetadata>>();

    public FileMetadataServiceTests()
    {
        mongoDatabase.GetCollection<StoredFileMetadata>(Arg.Any<string>()).Returns(collection);
    }

    [Fact]
    public async Task CreateAsync_ShouldCallInsertAsync_WithValidData()
    {
        FileMetadataService fileMetadataService = new(nullLogger, cacheService, mongoDatabase);

        _ = await fileMetadataService.CreateAsync("filename.tst", "text/plain", CancellationToken.None);

        await collection.Received(1).InsertOneAsync(Arg.Is<StoredFileMetadata>(x => x.FileName == "filename.tst"));
    }

    [Fact]
    public async Task ExistAsync_ShouldReturnFalse_WhenFileIdInvalid()
    {
        FileMetadataService fileMetadataService = new(nullLogger, cacheService, mongoDatabase);

        bool exists = await fileMetadataService.ExistAsync("testId");

        Assert.False(exists);
    }

    [Fact]
    public async Task ExistAsync_ShouldLimitCountToOne_WhenCalled()
    {
        FileMetadataService fileMetadataService = new(nullLogger, cacheService, mongoDatabase);

        _ = await fileMetadataService.ExistAsync(ObjectId.GenerateNewId().ToString());

        await collection.Received(1)
            .CountDocumentsAsync(Arg.Any<FilterDefinition<StoredFileMetadata>>(),
                Arg.Is<CountOptions>(x => x.Limit == 1));
    }

    [Fact]
    public async Task ExistAsync_ShouldReturnTrue_WhenFileIdValid()
    {
        FileMetadataService fileMetadataService = new(nullLogger, cacheService, mongoDatabase);
        string fileId = ObjectId.GenerateNewId().ToString();
        collection.CountDocumentsAsync(Arg.Any<FilterDefinition<StoredFileMetadata>>(),
                Arg.Is<CountOptions>(x => x.Limit == 1))
                .Returns(1);

        bool exist = await fileMetadataService.ExistAsync(fileId);

        Assert.True(exist);
    }
}
