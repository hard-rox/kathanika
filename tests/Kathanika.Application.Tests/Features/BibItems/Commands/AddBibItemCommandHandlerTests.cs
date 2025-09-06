using Kathanika.Application.Features.BibItems.Commands;
using Kathanika.Domain.Aggregates.BibItemAggregate;

namespace Kathanika.Application.Tests.Features.BibItems.Commands;

public class AddBibItemCommandHandlerTests
{
    private readonly IBibItemRepository _bibItemRepository;
    private readonly AddBibItemCommandHandler _handler;

    public AddBibItemCommandHandlerTests()
    {
        _bibItemRepository = Substitute.For<IBibItemRepository>();
        _handler = new AddBibItemCommandHandler(_bibItemRepository);
    }

    [Fact]
    public async Task Handle_ValidCommand_ShouldReturnSuccessResult()
    {
        // Arrange
        AddBibItemCommand command = new(
            "bib-123",
            "123456789",
            "QA76.73.C153",
            "Main Library",
            ItemType.Book,
            "Good condition",
            "Test notes");

        // Act
        KnResult<BibItem> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(command.BibRecordId, result.Value.BibRecordId);
        Assert.Equal(command.Barcode, result.Value.Barcode);
        Assert.Equal(command.CallNumber, result.Value.CallNumber);
        Assert.Equal(command.Location, result.Value.Location);
        Assert.Equal(command.ItemType, result.Value.ItemType);

        await _bibItemRepository.Received(1).AddAsync(Arg.Any<BibItem>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WithoutOptionalParameters_ShouldCreateBibItemWithDefaults()
    {
        // Arrange
        AddBibItemCommand command = new(
            "bib-123",
            "123456789",
            "QA76.73.C153",
            "Main Library",
            ItemType.Book);

        // Act
        KnResult<BibItem> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.Equal(ItemStatus.Available, result.Value.Status); // Default status
        Assert.Null(result.Value.Vendor); // No vendor support in current implementation

        await _bibItemRepository.Received(1).AddAsync(Arg.Any<BibItem>(), Arg.Any<CancellationToken>());
    }
}