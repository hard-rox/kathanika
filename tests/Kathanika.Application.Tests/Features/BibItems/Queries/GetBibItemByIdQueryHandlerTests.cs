using Kathanika.Application.Features.BibItems.Queries;
using Kathanika.Domain.Aggregates.BibItemAggregate;

namespace Kathanika.Application.Tests.Features.BibItems.Queries;

public class GetBibItemByIdQueryHandlerTests
{
    private readonly IBibItemRepository _bibItemRepository;
    private readonly GetBibItemByIdQueryHandler _handler;

    public GetBibItemByIdQueryHandlerTests()
    {
        _bibItemRepository = Substitute.For<IBibItemRepository>();
        _handler = new GetBibItemByIdQueryHandler(_bibItemRepository);
    }

    [Fact]
    public async Task Handle_ExistingBibItem_ShouldReturnSuccessResult()
    {
        // Arrange
        BibItem bibItem = CreateTestBibItem();
        GetBibItemByIdQuery query = new("item-123");

        _bibItemRepository.GetByIdAsync("item-123", Arg.Any<CancellationToken>())
            .Returns(bibItem);

        // Act
        KnResult<BibItem> result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(bibItem.BibRecordId, result.Value.BibRecordId);
        Assert.Equal(bibItem.Barcode, result.Value.Barcode);
        Assert.Equal(bibItem.CallNumber, result.Value.CallNumber);
        await _bibItemRepository.Received(1).GetByIdAsync("item-123", Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_NonExistentBibItem_ShouldReturnFailureResult()
    {
        // Arrange
        GetBibItemByIdQuery query = new("non-existent");

        _bibItemRepository.GetByIdAsync("non-existent", Arg.Any<CancellationToken>())
            .Returns((BibItem?)null);

        // Act
        KnResult<BibItem> result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(BibItemAggregateErrors.NotFound, result.Errors[0]);
    }

    [Fact]
    public async Task Handle_ValidId_ShouldCallRepositoryOnce()
    {
        // Arrange
        BibItem bibItem = CreateTestBibItem();
        GetBibItemByIdQuery query = new("item-123");

        _bibItemRepository.GetByIdAsync("item-123", Arg.Any<CancellationToken>())
            .Returns(bibItem);

        // Act
        await _handler.Handle(query, CancellationToken.None);

        // Assert
        await _bibItemRepository.Received(1).GetByIdAsync("item-123", Arg.Any<CancellationToken>());
    }

    private static BibItem CreateTestBibItem()
    {
        return BibItem.Create(
            "bib-123",
            "123456789",
            "QA76.73.C153",
            "Main Library",
            ItemType.Book,
            ItemStatus.Available).Value;
    }
}