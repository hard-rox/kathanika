using Kathanika.Application.Features.BibItems.Commands;
using Kathanika.Domain.Aggregates.BibItemAggregate;
using Kathanika.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Application.Tests.Features.BibItems.Commands;

public class UpdateBibItemCommandHandlerTests
{
    private readonly IBibItemRepository _bibItemRepository;
    private readonly UpdateBibItemCommandHandler _handler;

    public UpdateBibItemCommandHandlerTests()
    {
        _bibItemRepository = Substitute.For<IBibItemRepository>();
        Substitute.For<IVendorRepository>();
        _handler = new UpdateBibItemCommandHandler(_bibItemRepository);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldReturnSuccessResult()
    {
        // Arrange
        BibItem bibItem = CreateTestBibItem();
        UpdateBibItemCommand command = new(
            "item-123",
            "987654321",
            "QA76.73.NEW",
            "Reference Library",
            ItemType.Journal,
            ItemStatus.Available);

        _bibItemRepository.GetByIdAsync("item-123", Arg.Any<CancellationToken>())
            .Returns(bibItem);

        // Act
        KnResult<BibItem> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(command.Barcode, result.Value.Barcode);
        Assert.Equal(command.CallNumber, result.Value.CallNumber);
        Assert.Equal(command.Location, result.Value.Location);
        Assert.Equal(command.ItemType, result.Value.ItemType);

        await _bibItemRepository.Received(1).UpdateAsync(bibItem, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_NonExistentBibItem_ShouldReturnFailureResult()
    {
        // Arrange
        UpdateBibItemCommand command = new(
            "non-existent",
            "987654321",
            "QA76.73.NEW",
            "Reference Library",
            ItemType.Journal,
            ItemStatus.Available);

        _bibItemRepository.GetByIdAsync("non-existent", Arg.Any<CancellationToken>())
            .Returns((BibItem?)null);

        // Act
        KnResult<BibItem> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(BibItemAggregateErrors.NotFound, result.Errors[0]);
    }

    [Fact]
    public async Task Handle_WithInvalidBarcode_ShouldReturnValidationFailure()
    {
        // Arrange
        BibItem bibItem = CreateTestBibItem();
        UpdateBibItemCommand command = new(
            "item-123",
            "",
            "QA76.73.NEW",
            "Reference Library",
            ItemType.Journal,
            ItemStatus.Available);

        _bibItemRepository.GetByIdAsync("item-123", Arg.Any<CancellationToken>())
            .Returns(bibItem);

        // Act
        KnResult<BibItem> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, error => error.Code == "BibItem.BarcodeIsEmpty");
    }

    [Fact]
    public async Task Handle_WithEmptyCallNumber_ShouldReturnValidationFailure()
    {
        // Arrange
        BibItem bibItem = CreateTestBibItem();
        UpdateBibItemCommand command = new(
            "item-123",
            "987654321",
            "", // Empty call number
            "Reference Library",
            ItemType.Journal,
            ItemStatus.Available);

        _bibItemRepository.GetByIdAsync("item-123", Arg.Any<CancellationToken>())
            .Returns(bibItem);

        // Act
        KnResult<BibItem> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Contains(result.Errors, error => error.Code == "BibItem.CallNumberIsEmpty");
    }

    [Fact]
    public async Task Handle_WithOptionalFields_ShouldUpdateAllFields()
    {
        // Arrange
        BibItem bibItem = CreateTestBibItem();
        UpdateBibItemCommand command = new(
            "item-123",
            "987654321",
            "QA76.73.NEW",
            "Reference Library",
            ItemType.Journal,
            ItemStatus.Available,
            "Excellent condition",
            "Updated notes");

        _bibItemRepository.GetByIdAsync("item-123", Arg.Any<CancellationToken>())
            .Returns(bibItem);

        // Act
        KnResult<BibItem> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(command.ConditionNote, result.Value.ConditionNote);
        Assert.Equal(command.Notes, result.Value.Notes);
    }

    [Fact]
    public async Task Handle_ShouldPassCancellationTokenToRepository()
    {
        // Arrange
        BibItem bibItem = CreateTestBibItem();
        using CancellationTokenSource cancellationTokenSource = new();
        CancellationToken cancellationToken = cancellationTokenSource.Token;

        UpdateBibItemCommand command = new(
            "item-123",
            "987654321",
            "QA76.73.NEW",
            "Reference Library",
            ItemType.Journal,
            ItemStatus.Available);

        _bibItemRepository.GetByIdAsync("item-123", Arg.Any<CancellationToken>())
            .Returns(bibItem);

        // Act
        await _handler.Handle(command, cancellationToken);

        // Assert
        await _bibItemRepository.Received(1).GetByIdAsync("item-123", cancellationToken);
        await _bibItemRepository.Received(1).UpdateAsync(bibItem, cancellationToken);
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