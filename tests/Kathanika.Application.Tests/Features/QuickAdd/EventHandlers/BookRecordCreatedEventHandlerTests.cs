using Kathanika.Application.Features.QuickAdd.EventHandlers;
using Kathanika.Domain.Aggregates.BibItemAggregate;
using Kathanika.Domain.Aggregates.BibRecordAggregate;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Kathanika.Application.Tests.Features.QuickAdd.EventHandlers;

public class BookRecordCreatedEventHandlerTests
{
    private readonly IBibItemRepository _bibItemRepository;
    private readonly ILogger<BookRecordCreatedEventHandler> _logger;
    private readonly BookRecordCreatedEventHandler _sut;

    public BookRecordCreatedEventHandlerTests()
    {
        _bibItemRepository = Substitute.For<IBibItemRepository>();
        _logger = new NullLogger<BookRecordCreatedEventHandler>();
        _sut = new BookRecordCreatedEventHandler(_bibItemRepository, _logger);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(null)]
    public async Task Handle_WithZeroOrNullCopies_ShouldNotCreateItems(int? numberOfCopies)
    {
        // Arrange
        var notification = new BookRecordCreatedEvent("test-id", numberOfCopies);

        // Act
        await _sut.Handle(notification, CancellationToken.None);

        // Assert
        await _bibItemRepository.DidNotReceive()
            .AddAsync(Arg.Any<IEnumerable<BibItem>>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_WithValidNumberOfCopies_ShouldCreateItems()
    {
        // Arrange
        var notification = new BookRecordCreatedEvent("test-id", 2);
        IEnumerable<BibItem>? capturedItems = null;

        _bibItemRepository
            .AddAsync(Arg.Do<IEnumerable<BibItem>>(items => capturedItems = items), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        // Act
        await _sut.Handle(notification, CancellationToken.None);

        // Assert
        await _bibItemRepository.Received(1)
            .AddAsync(Arg.Any<IEnumerable<BibItem>>(), Arg.Any<CancellationToken>());
        Assert.NotNull(capturedItems);
        Assert.Equal(2, capturedItems.Count());
    }

    [Fact]
    public async Task Handle_WithMultipleCopies_ShouldCreateItemsWithCorrectBarcodeFormat()
    {
        // Arrange
        var notification = new BookRecordCreatedEvent("test-id", 2);
        IEnumerable<BibItem>? capturedItems = null;

        _bibItemRepository
            .AddAsync(Arg.Do<IEnumerable<BibItem>>(items => capturedItems = items), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        // Act
        await _sut.Handle(notification, CancellationToken.None);

        // Assert
        Assert.NotNull(capturedItems);
        var itemsList = capturedItems.ToList();
        Assert.Equal(2, itemsList.Count);
        Assert.Equal($"BK-{DateTime.Today.Year}-MAIN-0001", itemsList[0].Barcode);
        Assert.Equal($"BK-{DateTime.Today.Year}-MAIN-0002", itemsList[1].Barcode);
    }

    [Fact]
    public async Task Handle_WithMultipleCopies_ShouldCreateItemsWithCorrectCallNumberFormat()
    {
        // Arrange
        var notification = new BookRecordCreatedEvent("test-id", 2);
        IEnumerable<BibItem>? capturedItems = null;

        _bibItemRepository
            .AddAsync(Arg.Do<IEnumerable<BibItem>>(items => capturedItems = items), Arg.Any<CancellationToken>())
            .Returns(Task.CompletedTask);

        // Act
        await _sut.Handle(notification, CancellationToken.None);

        // Assert
        Assert.NotNull(capturedItems);
        var itemsList = capturedItems.ToList();
        Assert.Equal(2, itemsList.Count);
        Assert.Equal("DM 0001", itemsList[0].CallNumber);
        Assert.Equal("DM 0002", itemsList[1].CallNumber);
    }

    [Fact]
    public async Task Handle_WhenItemCreationFails_ShouldLogError()
    {
        // Arrange
        var notification = new BookRecordCreatedEvent("test-id", 2);

        _bibItemRepository
            .When(x => x.AddAsync(Arg.Any<IEnumerable<BibItem>>(), Arg.Any<CancellationToken>()))
            .Throw(new Exception("Failed to create items"));

        // Act
        await _sut.Handle(notification, CancellationToken.None);

        // Assert
        _logger.Received(1).LogError("Failed to create book items");
    }
}