using Kathanika.Application.Services;
using Kathanika.Domain.Primitives;
using Kathanika.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MongoDB.Driver;

namespace Kathanika.UnitTests.PersistenceUnitTests;

public sealed class BaseRepositoryTests
{
    public class DummyAggregate : AggregateRoot
    {
        public string? Name { get; set; }
    };
    private class DummyRepo : Repository<DummyAggregate>
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
    private readonly Mock<IMongoCollection<DummyAggregate>> collectionMock = new();
    private readonly Mock<IAsyncCursor<DummyAggregate>> cursorMock = new();

    public BaseRepositoryTests()
    {
        databaseMock.Setup(x => x.GetCollection<DummyAggregate>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()))
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
        collectionMock.Setup(x => x.FindAsync(It.IsAny<FilterDefinition<DummyAggregate>>(),
            It.IsAny<FindOptions<DummyAggregate, DummyAggregate>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(cursorMock.Object)
            .Verifiable();

        var repo = new DummyRepo(databaseMock.Object, "", nullLogger, cacheMock.Object);

        // Act
        var result = await repo.GetByIdAsync("6487aceb533e0377b58d501c");

        // Assert
        collectionMock.Verify(x => x.FindAsync(It.IsAny<FilterDefinition<DummyAggregate>>(),
            It.Is<FindOptions<DummyAggregate, DummyAggregate>>(x => x == null),
            It.Is<CancellationToken>(x => x == default)), Times.Exactly(1));
    }

    [Fact]
    public async Task ListAllAsync_Should_Call_FindAsync_With_EmptyFilter()
    {
        // Arrange
        collectionMock.Setup(x => x.FindAsync(It.IsAny<FilterDefinition<DummyAggregate>>(),
            It.IsAny<FindOptions<DummyAggregate, DummyAggregate>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(cursorMock.Object)
            .Verifiable();

        var repo = new DummyRepo(databaseMock.Object, "", nullLogger, cacheMock.Object);

        // Act
        var result = await repo.ListAllAsync();

        // Assert
        collectionMock.Verify(x => x.FindAsync(It.Is<FilterDefinition<DummyAggregate>>(x => x == Builders<DummyAggregate>.Filter.Empty),
            It.Is<FindOptions<DummyAggregate, DummyAggregate>>(x => x == null),
            It.IsAny<CancellationToken>()), Times.Exactly(1));
    }

    [Fact]
    public async Task ListAllAsync_Should_Call_FindAsync_With_Expression()
    {
        // Arrange
        collectionMock.Setup(x => x.FindAsync(It.IsAny<FilterDefinition<DummyAggregate>>(),
            It.IsAny<FindOptions<DummyAggregate, DummyAggregate>>(),
            It.IsAny<CancellationToken>()))
            .ReturnsAsync(cursorMock.Object)
            .Verifiable();

        var repo = new DummyRepo(databaseMock.Object, "", nullLogger, cacheMock.Object);

        // Act
        var result = await repo.ListAllAsync(x => x.Id == "1");

        // Assert
        collectionMock.Verify(x => x.FindAsync(It.IsAny<FilterDefinition<DummyAggregate>>(),
            It.Is<FindOptions<DummyAggregate, DummyAggregate>>(x => x == null),
            It.IsAny<CancellationToken>()), Times.Exactly(1));
    }

    [Fact]
    public async Task AddAsync_Should_Call_InsertOneAsync()
    {
        // Arrange
        var aggregate = new DummyAggregate() { Name = "" };
        collectionMock.Setup(x => x.InsertOneAsync(It.IsAny<DummyAggregate>(),
            It.IsAny<InsertOneOptions>(),
            It.IsAny<CancellationToken>()))
            .Verifiable();

        var repo = new DummyRepo(databaseMock.Object, "", nullLogger, cacheMock.Object);

        // Act
        var result = await repo.AddAsync(aggregate);

        // Assert
        collectionMock.Verify(x => x.InsertOneAsync(It.Is<DummyAggregate>(x => x == aggregate),
            It.Is<InsertOneOptions>(x => x == null), It.Is<CancellationToken>(x => x == default)), Times.Exactly(1));
    }

    [Fact]
    public async Task UpdateAsync_Should_Call_ReplaceOneAsync()
    {
        // Arrange
        var aggregate = new DummyAggregate() { Name = "" };
        collectionMock.Setup(x => x.ReplaceOneAsync(It.IsAny<FilterDefinition<DummyAggregate>>(),
            It.IsAny<DummyAggregate>(),
            It.IsAny<ReplaceOptions>(),
            It.IsAny<CancellationToken>()))
            .Verifiable();

        var repo = new DummyRepo(databaseMock.Object, "", nullLogger, cacheMock.Object);

        // Act
        await repo.UpdateAsync(aggregate);

        // Assert
        collectionMock.Verify(x => x.ReplaceOneAsync(It.IsAny<FilterDefinition<DummyAggregate>>(),
            It.Is<DummyAggregate>(x => x == aggregate),
            It.Is<ReplaceOptions>(x => x == null),
            It.Is<CancellationToken>(x => x == default)), Times.Exactly(1));
    }

    [Fact]
    public async Task DeleteAsync_Should_Call_DeleteOneAsync()
    {
        // Arrange
        collectionMock.Setup(x => x.DeleteOneAsync(It.IsAny<FilterDefinition<DummyAggregate>>(),
            It.IsAny<CancellationToken>()))
            .Verifiable();

        var repo = new DummyRepo(databaseMock.Object, "", nullLogger, cacheMock.Object);

        // Act
        await repo.DeleteAsync("6487aceb533e0377b58d501c");

        // Assert
        collectionMock.Verify(x => x.DeleteOneAsync(It.IsAny<FilterDefinition<DummyAggregate>>(),
            It.Is<CancellationToken>(x => x == default)), Times.Exactly(1));
    }
}
