using Kathanika.Application.Services;
using Kathanika.Domain.Primitives;
using Kathanika.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Moq;

namespace Kathanika.UnitTests.PersistenceUnitTests;

public sealed class BaseRepositoryTests
{
    private class DummyEntity : AggregateRoot { public string? Name { get; set; } };
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

    private readonly Mock<IMongoDatabase> databaseMock = new();
    private readonly Mock<ILogger<DummyRepo>> loggerMock = new();
    private readonly Mock<ICacheService> cacheMock = new();

    [Fact]
    public void AsQueryable_Should_Return_Queryable()
    {
        // Arrange
        var collection = databaseMock.Object.GetCollection<DummyEntity>("dummyCollection");

        var dummyData = new List<DummyEntity>
        {
            new DummyEntity { Name = "Hello 1"},
            new DummyEntity { Name = "Hello 2"},
            new DummyEntity { Name = "Hello 3"}
        };
        collection.InsertMany(dummyData);

        var repo = new DummyRepo(databaseMock.Object, "dummyCollection", loggerMock.Object, cacheMock.Object);

        // Act
        var result = repo.AsQueryable();

        // Assert
        Assert.IsType<IQueryable<DummyEntity>>(result);
    }
}
