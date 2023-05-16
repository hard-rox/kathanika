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
    public class DummyEntity : AggregateRoot { public string? Name { get; set; } };
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

    private readonly Mock<IMongoDatabase> _databaseMock = new();
    private readonly Mock<IMongoCollection<DummyEntity>> _collectionMock = new();
    private readonly Mock<ICacheService> _cacheMock = new();
    private readonly ILogger<DummyRepo> _logger = new NullLogger<DummyRepo>();

    [Fact]
    public void AsQueryable_Should_Return_Queryable()
    {
        // Arrange
        _databaseMock.Setup(x => x.GetCollection<DummyEntity>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_collectionMock.Object);
        var repo = new DummyRepo(_databaseMock.Object, "dummyCollection", _logger, _cacheMock.Object);

        // Act
        var result = repo.AsQueryable();

        // Assert
        _databaseMock.Verify(x => x.GetCollection<It.IsSubtype<DummyEntity>>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()), Times.Once);
        _collectionMock.Verify(x => x.AsQueryable(null), Times.Once);
    }
}
