using Kathanika.Application.Features.BibItems.Commands;
using Kathanika.Domain.Aggregates.BibItemAggregate;

namespace Kathanika.Application.Tests.Features.BibItems.Commands;

public class CheckInBibItemCommandHandlerTests
{
    private readonly IBibItemRepository _bibItemRepository;
    private readonly CheckInBibItemCommandHandler _handler;

    public CheckInBibItemCommandHandlerTests()
    {
        _bibItemRepository = Substitute.For<IBibItemRepository>();
        _handler = new CheckInBibItemCommandHandler(_bibItemRepository);
    }

    [Fact]
    public async Task Handle_CheckedOutItem_ShouldReturnSuccessResult()
    {
        // Arrange
        BibItem bibItem = CreateTestBibItem();
        bibItem.CheckOut(); // First check out the item
        CheckInBibItemCommand command = new("item-123");

        _bibItemRepository.GetByIdAsync("item-123", Arg.Any<CancellationToken>())
            .Returns(bibItem);

        // Act
        KnResult<BibItem> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ItemStatus.Available, result.Value.Status);
        Assert.NotNull(result.Value.LastCheckInDate);
        await _bibItemRepository.Received(1).UpdateAsync(bibItem, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_NotCheckedOutItem_ShouldReturnFailureResult()
    {
        // Arrange
        BibItem bibItem = CreateTestBibItem(); // Item is available, not checked out
        CheckInBibItemCommand command = new("item-123");

        _bibItemRepository.GetByIdAsync("item-123", Arg.Any<CancellationToken>())
            .Returns(bibItem);

        // Act
        KnResult<BibItem> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(BibItemAggregateErrors.InvalidStatus, result.Errors[0]);
    }

    [Fact]
    public async Task Handle_NonExistentBibItem_ShouldReturnFailureResult()
    {
        // Arrange
        CheckInBibItemCommand command = new("non-existent");

        _bibItemRepository.GetByIdAsync("non-existent", Arg.Any<CancellationToken>())
            .Returns((BibItem?)null);

        // Act
        KnResult<BibItem> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(BibItemAggregateErrors.NotFound, result.Errors[0]);
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