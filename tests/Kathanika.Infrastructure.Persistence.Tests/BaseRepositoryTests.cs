using System.Diagnostics.CodeAnalysis;
using Kathanika.Domain.Primitives;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MongoDB.Driver;

namespace Kathanika.Infrastructure.Persistence.Tests;

public sealed class BaseRepositoryTests
{
    private readonly HybridCache _cache = Substitute.For<HybridCache>();
    private readonly IMongoCollection<DummyAggregate> _collection = Substitute.For<IMongoCollection<DummyAggregate>>();
    private readonly IMongoDatabase _database = Substitute.For<IMongoDatabase>();

    private readonly ILogger<DummyRepo> _nullLogger = new NullLogger<DummyRepo>();

    public BaseRepositoryTests()
    {
        _database.GetCollection<DummyAggregate>(Arg.Any<string>(), Arg.Any<MongoCollectionSettings>())
            .Returns(_collection);
    }

    [Fact]
    public async Task ListAllAsync_Should_Call_FindAsync_With_EmptyFilter()
    {
        DummyRepo repo = new(_database, "", _nullLogger, _cache);

        // Act
        await repo.ListAllAsync();

        // Assert
        await _collection.Received(1)
            .FindAsync(Arg.Is<FilterDefinition<DummyAggregate>>(x => x == Builders<DummyAggregate>.Filter.Empty),
                Arg.Is<FindOptions<DummyAggregate, DummyAggregate>>(x => x == null),
                Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task ListAllAsync_Should_Call_FindAsync_With_Expression()
    {
        DummyRepo repo = new(_database, "", _nullLogger, _cache);

        // Act
        await repo.ListAllAsync(x => x.Id == "1");

        // Assert
        await _collection.Received(1).FindAsync(Arg.Any<FilterDefinition<DummyAggregate>>(),
            Arg.Is<FindOptions<DummyAggregate, DummyAggregate>>(x => x == null),
            Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task AddAsync_Should_Call_InsertOneAsync()
    {
        // Arrange
        DummyAggregate aggregate = new();
        DummyRepo repo = new(_database, "", _nullLogger, _cache);

        // Act
        await repo.AddAsync(aggregate);

        // Assert
        await _collection.Received(1).InsertOneAsync(Arg.Is<DummyAggregate>(x => x == aggregate),
            Arg.Is<InsertOneOptions>(x => x == null), Arg.Is<CancellationToken>(x => x == default));
    }

    [Fact]
    public async Task UpdateAsync_Should_Call_ReplaceOneAsync()
    {
        // Arrange
        DummyAggregate aggregate = new();
        DummyRepo repo = new(_database, "", _nullLogger, _cache);

        // Act
        await repo.UpdateAsync(aggregate);

        // Assert
        await _collection.Received(1).ReplaceOneAsync(Arg.Any<FilterDefinition<DummyAggregate>>(),
            Arg.Is<DummyAggregate>(x => x == aggregate),
            Arg.Is<ReplaceOptions>(x => x == null),
            Arg.Is<CancellationToken>(x => x == default));
    }

    [Fact]
    public async Task DeleteAsync_Should_Call_DeleteOneAsync()
    {
        // Arrange
        DummyRepo repo = new(_database, "", _nullLogger, _cache);

        // Act
        await repo.DeleteAsync(Guid.NewGuid().ToString());

        // Assert
        await _collection.Received(1).DeleteOneAsync(Arg.Any<FilterDefinition<DummyAggregate>>(),
            Arg.Is<CancellationToken>(x => x == default));
    }

    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    public class DummyAggregate : AggregateRoot;

    private class DummyRepo(
        IMongoDatabase database,
        string collectionName,
        ILogger<DummyRepo> logger,
        HybridCache cacheService) : Repository<DummyAggregate>(database, collectionName, logger, cacheService);
}