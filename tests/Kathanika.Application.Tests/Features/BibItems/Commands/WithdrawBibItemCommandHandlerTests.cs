using Kathanika.Application.Features.BibItems.Commands;
using Kathanika.Domain.Aggregates.BibItemAggregate;

namespace Kathanika.Application.Tests.Features.BibItems.Commands;

public class WithdrawBibItemCommandHandlerTests
{
    private readonly IBibItemRepository _bibItemRepository;
    private readonly WithdrawBibItemCommandHandler _handler;

    public WithdrawBibItemCommandHandlerTests()
    {
        _bibItemRepository = Substitute.For<IBibItemRepository>();
        _handler = new WithdrawBibItemCommandHandler(_bibItemRepository);
    }

    [Fact]
    public async Task Handle_ValidItem_ShouldReturnSuccessResult()
    {
        // Arrange
        BibItem bibItem = CreateTestBibItem();
        WithdrawBibItemCommand command = new("item-123", "Damaged beyond repair");

        _bibItemRepository.GetByIdAsync("item-123", Arg.Any<CancellationToken>())
            .Returns(bibItem);

        // Act
        KnResult<BibItem> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ItemStatus.Withdrawn, result.Value.Status);
        Assert.NotNull(result.Value.WithdrawnDate);
        Assert.Contains("Damaged beyond repair", result.Value.Notes);
        await _bibItemRepository.Received(1).UpdateAsync(bibItem, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WithoutReason_ShouldWithdrawSuccessfully()
    {
        // Arrange
        BibItem bibItem = CreateTestBibItem();
        WithdrawBibItemCommand command = new("item-123");

        _bibItemRepository.GetByIdAsync("item-123", Arg.Any<CancellationToken>())
            .Returns(bibItem);

        // Act
        KnResult<BibItem> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(ItemStatus.Withdrawn, result.Value.Status);
        Assert.NotNull(result.Value.WithdrawnDate);
    }

    [Fact]
    public async Task Handle_NonExistentBibItem_ShouldReturnFailureResult()
    {
        // Arrange
        WithdrawBibItemCommand command = new("non-existent", "Test reason");

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
