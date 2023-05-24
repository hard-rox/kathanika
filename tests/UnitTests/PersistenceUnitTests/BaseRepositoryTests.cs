using Kathanika.Application.Services;
using Kathanika.Domain.Primitives;
using Kathanika.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MongoDB.Driver;
using Moq;

namespace Kathanika.UnitTests.PersistenceUnitTests;

public sealed class BaseRepositoryTests
{
    public class DummyEntity : AggregateRoot
    {
        public new string Id { get; set; } = string.Empty;
        public string? Name { get; set; }
    };
    private class DummyRepo : Repository<DummyEntity>
    {
        public DummyRepo(IMongoDatabase database,
            string collectionName,
            ILogger<DummyRepo> logger,
            ICacheService cacheService)
        : base(database, collectionName, logger, cacheService)
        {
        }
    }

    private readonly ILogger<DummyRepo> nullLogger = new NullLogger<DummyRepo>();
    private readonly Mock<ICacheService> cacheMock = new();
    private readonly Mock<IMongoDatabase> databaseMock = new();
    private readonly Mock<IMongoCollection<DummyEntity>> collectionMock = new();
    private readonly Mock<IAsyncCursor<DummyEntity>> cursorMock = new();

    [Fact]
    public async Task GetById_Should_Return_One_When_ValidId()
    {
        // Arrange
        var dummyData = new List<DummyEntity>
        {
            new DummyEntity { Id = "1", Name = "Hello 1"},
            // new DummyEntity { Id = "2", Name = "Hello 2"},
            // new DummyEntity { Id = "3", Name = "Hello 3"}
        };

        databaseMock.Setup(x => x.GetCollection<DummyEntity>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()))
            .Returns(collectionMock.Object);
        cursorMock.Setup(x => x.Current).Returns(dummyData);
        cursorMock.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
            .Returns(true).Returns(false);
        cursorMock.SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true).ReturnsAsync(false);
        collectionMock.Setup(x => x.FindAsync(It.IsAny<FilterDefinition<DummyEntity>>(),
            It.IsAny<FindOptions<DummyEntity, DummyEntity>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(cursorMock.Object);

        var repo = new DummyRepo(databaseMock.Object, "dummycollection", nullLogger, cacheMock.Object);

        // Act
            var result = await repo.GetByIdAsync("1");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("1", result.Id);
    }
}
