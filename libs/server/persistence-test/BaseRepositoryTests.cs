using Kathanika.Application.Services;
using Kathanika.Domain.Primitives;
using Kathanika.Persistence;
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
    private readonly ICacheService cache = Substitute.For<ICacheService>();
    private readonly IMongoDatabase database = Substitute.For<IMongoDatabase>();
    private readonly IMongoCollection<DummyAggregate> collection = Substitute.For<IMongoCollection<DummyAggregate>>();

    public BaseRepositoryTests()
    {
        database.GetCollection<DummyAggregate>(Arg.Any<string>(), Arg.Any<MongoCollectionSettings>())
            .Returns(collection);
    }

    [Fact]
    public async Task GetById_Should_Call_FindAsync()
    {
        DummyRepo repo = new(database, "", nullLogger, cache);

        // Act
        await repo.GetByIdAsync("6487aceb533e0377b58d501c");

        // Assert
        await collection.Received(1).FindAsync(Arg.Any<FilterDefinition<DummyAggregate>>(),
            Arg.Is<FindOptions<DummyAggregate, DummyAggregate>>(x => x == null),
            Arg.Is<CancellationToken>(x => x == default));
    }

    [Fact]
    public async Task ListAllAsync_Should_Call_FindAsync_With_EmptyFilter()
    {
        DummyRepo repo = new(database, "", nullLogger, cache);

        // Act
        await repo.ListAllAsync();

        // Assert
        await collection.Received(1)
            .FindAsync(Arg.Is<FilterDefinition<DummyAggregate>>(x => x == Builders<DummyAggregate>.Filter.Empty),
                Arg.Is<FindOptions<DummyAggregate, DummyAggregate>>(x => x == null),
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ListAllAsync_Should_Call_FindAsync_With_Expression()
    {
        DummyRepo repo = new(database, "", nullLogger, cache);

        // Act
        await repo.ListAllAsync(x => x.Id == "1");

        // Assert
        await collection.Received(1).FindAsync(Arg.Any<FilterDefinition<DummyAggregate>>(),
            Arg.Is<FindOptions<DummyAggregate, DummyAggregate>>(x => x == null),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task AddAsync_Should_Call_InsertOneAsync()
    {
        // Arrange
        DummyAggregate aggregate = new() { Name = "" };
        DummyRepo repo = new(database, "", nullLogger, cache);

        // Act
        await repo.AddAsync(aggregate);

        // Assert
        await collection.Received(1).InsertOneAsync(Arg.Is<DummyAggregate>(x => x == aggregate),
            Arg.Is<InsertOneOptions>(x => x == null), Arg.Is<CancellationToken>(x => x == default));
    }

    [Fact]
    public async Task UpdateAsync_Should_Call_ReplaceOneAsync()
    {
        // Arrange
        DummyAggregate aggregate = new() { Name = "" };
        DummyRepo repo = new(database, "", nullLogger, cache);

        // Act
        await repo.UpdateAsync(aggregate);

        // Assert
        await collection.Received(1).ReplaceOneAsync(Arg.Any<FilterDefinition<DummyAggregate>>(),
            Arg.Is<DummyAggregate>(x => x == aggregate),
            Arg.Is<ReplaceOptions>(x => x == null),
            Arg.Is<CancellationToken>(x => x == default));
    }

    [Fact]
    public async Task DeleteAsync_Should_Call_DeleteOneAsync()
    {
        // Arrange
        DummyRepo repo = new(database, "", nullLogger, cache);

        // Act
        await repo.DeleteAsync("6487aceb533e0377b58d501c");

        // Assert
        await collection.Received(1).DeleteOneAsync(Arg.Any<FilterDefinition<DummyAggregate>>(),
            Arg.Is<CancellationToken>(x => x == default));
    }
}
