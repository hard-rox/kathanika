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

    public BaseRepositoryTests()
    {
        databaseMock.Setup(x => x.GetCollection<DummyEntity>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()))
            .Returns(collectionMock.Object);

        cursorMock.SetupSequence(x => x.MoveNext(It.IsAny<CancellationToken>()))
            .Returns(true).Returns(false);
        cursorMock.SetupSequence(x => x.MoveNextAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(true).ReturnsAsync(false);
    }

    [Fact]
    public async Task GetById_Should_Call_FindAsync()
    {
        // Arrange
        collectionMock.Setup(x => x.FindAsync(It.IsAny<FilterDefinition<DummyEntity>>(),
            It.IsAny<FindOptions<DummyEntity, DummyEntity>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(cursorMock.Object)
            .Verifiable();

        var repo = new DummyRepo(databaseMock.Object, "", nullLogger, cacheMock.Object);

        // Act
        var result = await repo.GetByIdAsync("1");

        // Assert
        collectionMock.Verify(x => x.FindAsync(It.IsAny<FilterDefinition<DummyEntity>>(),
            It.IsAny<FindOptions<DummyEntity, DummyEntity>>(),
            It.IsAny<CancellationToken>()), Times.Exactly(1));
    }
}
