using Kathanika.Application.Features.BibItems.Commands;
using Kathanika.Domain.Aggregates.BibItemAggregate;

namespace Kathanika.Application.Tests.Features.BibItems.Commands;

public class CheckOutBibItemCommandHandlerTests
{
    private readonly IBibItemRepository _bibItemRepository;
    private readonly CheckOutBibItemCommandHandler _handler;

    public CheckOutBibItemCommandHandlerTests()
    {
        _bibItemRepository = Substitute.For<IBibItemRepository>();
        _handler = new CheckOutBibItemCommandHandler(_bibItemRepository);
    }

    [Fact]
    public async Task Handle_AvailableItem_ShouldReturnSuccessResult()
    {
        // Arrange
        BibItem bibItem = CreateTestBibItem();
        CheckOutBibItemCommand command = new("item-123");

        _bibItemRepository.GetByIdAsync("item-123", Arg.Any<CancellationToken>())
            .Returns(bibItem);

        // Act
        KnResult<BibItem> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ItemStatus.CheckedOut, result.Value.Status);
        Assert.NotNull(result.Value.LastCheckOutDate);
        await _bibItemRepository.Received(1).UpdateAsync(bibItem, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_NonExistentBibItem_ShouldReturnFailureResult()
    {
        // Arrange
        CheckOutBibItemCommand command = new("non-existent");

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
            ItemType.Book).Value;
    }
}