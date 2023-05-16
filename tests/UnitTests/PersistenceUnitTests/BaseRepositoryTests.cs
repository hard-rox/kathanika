using Kathanika.Application.Services;
using Kathanika.Domain.Primitives;
using Kathanika.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using MongoDB.Driver;
using Moq;

namespace Kathanika.UnitTests.PersistenceUnitTests;

public class DummyEntity : AggregateRoot { public string? Name { get; set; } };
internal class DummyRepo : Repository<DummyEntity>
{
    public DummyRepo(IMongoDatabase database,
        string collectionName,
        ILogger<DummyRepo> logger,
        ICacheService cacheService)
    : base(database, collectionName, logger, cacheService)
    {
    }
}

public sealed class BaseRepositoryTests
{
    private readonly Mock<IMongoDatabase> _databaseMock = new();
    private readonly Mock<IMongoCollection<DummyEntity>> _collectionMock = new();
    private readonly Mock<ICacheService> _cacheMock = new();
    private readonly ILogger<DummyRepo> _logger = new NullLogger<DummyRepo>();

    // [Fact]
    // public async void GetById_Should_Return_With_Valid_Id()
    // {
    //     // Arrange
    //     var dummy = new DummyEntity() { Name = "Hello" };
    //     var mockFindFluent = new Mock<IFindFluent<DummyEntity, DummyEntity>>();
    //     mockFindFluent.Setup(x => x.SingleOrDefaultAsync(default)).Returns(await Task.Run(() => dummy));
    //     _collectionMock.Setup(x => x.Find(It.IsAny<FilterDefinition<DummyEntity>>(), It.IsAny<FindOptions>())).Returns(mockFindFluent.Object);
    //     _databaseMock.Setup(x => x.GetCollection<DummyEntity>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>())).Returns(_collectionMock.Object);

    //     var repo = new DummyRepo(_databaseMock.Object, "dummyCollection", _logger, _cacheMock.Object);

    //     // Act
    //     var result = repo.AsQueryable();

    //     // Assert
    //     _databaseMock.Verify(x => x.GetCollection<It.IsSubtype<DummyEntity>>(It.IsAny<string>(), It.IsAny<MongoCollectionSettings>()), Times.Once);
    //     _collectionMock.Verify(x => x.AsQueryable(null), Times.Once);
    // }
}
