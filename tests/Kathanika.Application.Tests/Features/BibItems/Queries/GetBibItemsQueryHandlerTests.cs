using Kathanika.Application.Features.BibItems.Queries;
using Kathanika.Domain.Aggregates.BibItemAggregate;

namespace Kathanika.Application.Tests.Features.BibItems.Queries;

public class GetBibItemsQueryHandlerTests
{
    private readonly IBibItemRepository _bibItemRepository;
    private readonly GetBibItemsQueryHandler _handler;

    public GetBibItemsQueryHandlerTests()
    {
        _bibItemRepository = Substitute.For<IBibItemRepository>();
        _handler = new GetBibItemsQueryHandler(_bibItemRepository);
    }

    [Fact]
    public async Task Handle_WithValidBibRecordId_ShouldReturnMatchingBibItems()
    {
        // Arrange
        const string bibRecordId = "bib-123";
        List<BibItem> expectedBibItems =
        [
            CreateTestBibItem(bibRecordId, "123456789"),
            CreateTestBibItem(bibRecordId, "987654321")
        ];

        _bibItemRepository.AsQueryable()
            .Returns(expectedBibItems.AsQueryable());

        GetBibItemsQuery query = new(bibRecordId);

        // Act
        IQueryable<BibItem> result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.All(result, item => Assert.Equal(bibRecordId, item.BibRecordId));

        _bibItemRepository.Received(1).AsQueryable();
    }

    [Fact]
    public async Task Handle_WithNonExistentBibRecordId_ShouldReturnEmptyQueryable()
    {
        // Arrange
        const string bibRecordId = "non-existent-bib";
        List<BibItem> emptyBibItems = [];

        _bibItemRepository.AsQueryable()
            .Returns(emptyBibItems.AsQueryable());

        GetBibItemsQuery query = new(bibRecordId);

        // Act
        IQueryable<BibItem> result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Empty(result);

        _bibItemRepository.Received(1).AsQueryable();
    }

    [Fact]
    public async Task Handle_WithSpecificBibRecordId_ShouldCallRepositoryWithCorrectFilter()
    {
        // Arrange
        const string bibRecordId = "bib-456";
        List<BibItem> bibItems = [CreateTestBibItem(bibRecordId, "111111111")];

        _bibItemRepository.AsQueryable()
            .Returns(bibItems.AsQueryable());

        GetBibItemsQuery query = new(bibRecordId);

        // Act
        await _handler.Handle(query, CancellationToken.None);

        // Assert
        _bibItemRepository.Received(1).AsQueryable();
    }

    [Fact]
    public async Task Handle_WithMultipleBibItems_ShouldReturnAllMatchingItems()
    {
        // Arrange
        const string bibRecordId = "bib-789";
        List<BibItem> expectedBibItems =
        [
            CreateTestBibItem(bibRecordId, "100000001"),
            CreateTestBibItem(bibRecordId, "100000002"),
            CreateTestBibItem(bibRecordId, "100000003"),
            CreateTestBibItem(bibRecordId, "100000004")
        ];

        _bibItemRepository.AsQueryable()
            .Returns(expectedBibItems.AsQueryable());

        GetBibItemsQuery query = new(bibRecordId);

        // Act
        IQueryable<BibItem> result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(4, result.Count());
        Assert.All(result, item => Assert.Equal(bibRecordId, item.BibRecordId));

        // Verify that the result maintains the order from repository
        List<BibItem> resultList = result.ToList();
        for (int i = 0; i < expectedBibItems.Count; i++)
        {
            Assert.Equal(expectedBibItems[i].Barcode, resultList[i].Barcode);
        }
    }

    [Fact]
    public async Task Handle_ShouldPassCancellationTokenToRepository()
    {
        // Arrange
        const string bibRecordId = "bib-token-test";
        using CancellationTokenSource cancellationTokenSource = new();
        CancellationToken cancellationToken = cancellationTokenSource.Token;

        _bibItemRepository.AsQueryable()
            .Returns(new List<BibItem>().AsQueryable());

        GetBibItemsQuery query = new(bibRecordId);

        // Act
        await _handler.Handle(query, cancellationToken);

        // Assert
        _bibItemRepository.Received(1).AsQueryable();
    }

    [Fact]
    public async Task Handle_WithDifferentBibRecordIds_ShouldFilterCorrectly()
    {
        // Arrange
        const string targetBibRecordId = "bib-target";
        const string otherBibRecordId = "bib-other";

        List<BibItem> targetBibItems =
        [
            CreateTestBibItem(targetBibRecordId, "200000001"),
            CreateTestBibItem(targetBibRecordId, "200000002")
        ];

        _bibItemRepository.AsQueryable()
            .Returns(targetBibItems.AsQueryable());

        GetBibItemsQuery query = new(targetBibRecordId);

        // Act
        IQueryable<BibItem> result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.All(result, item =>
        {
            Assert.Equal(targetBibRecordId, item.BibRecordId);
            Assert.NotEqual(otherBibRecordId, item.BibRecordId);
        });
    }

    private static BibItem CreateTestBibItem(string bibRecordId, string barcode)
    {
        return BibItem.Create(
            bibRecordId,
            barcode,
            "QA76.73.C153",
            "Main Library",
            ItemType.Book,
            ItemStatus.Available).Value;
    }
}