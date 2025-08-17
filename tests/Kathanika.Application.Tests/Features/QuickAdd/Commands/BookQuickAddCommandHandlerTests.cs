using System.Linq.Expressions;
using Kathanika.Application.Features.QuickAdd.Commands;
using Kathanika.Domain.Aggregates.BibItemAggregate;
using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Application.Tests.Features.QuickAdd.Commands;

public sealed class BookQuickAddCommandHandlerTests
{
    private readonly IBibRecordRepository _mockBibRecordRepository;
    private readonly IBibItemRepository _mockBibItemRepository;
    private readonly BookQuickAddCommandHandler _handler;
    private readonly Faker _faker;

    public BookQuickAddCommandHandlerTests()
    {
        _mockBibRecordRepository = Substitute.For<IBibRecordRepository>();
        _mockBibItemRepository = Substitute.For<IBibItemRepository>();
        _handler = new BookQuickAddCommandHandler(
            _mockBibRecordRepository,
            _mockBibItemRepository);
        _faker = new Faker();
    }

    [Fact]
    public async Task Handle_ShouldCreateBibRecord_WhenValidCommandProvided()
    {
        // Arrange
        BookQuickAddCommand command = CreateValidCommand();
        BibRecord mockBibRecord = CreateMockBibRecord(command);
        BibItem mockBibItem = CreateMockBibItem();

        _mockBibRecordRepository
            .AddAsync(Arg.Any<BibRecord>(), Arg.Any<CancellationToken>())
            .Returns(mockBibRecord);

        _mockBibItemRepository
            .ExistsAsync(Arg.Any<Expression<Func<BibItem, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(false);

        _mockBibItemRepository
            .AddAsync(Arg.Any<BibItem>(), Arg.Any<CancellationToken>())
            .Returns(mockBibItem);

        // Act
        KnResult<BibRecord> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        await _mockBibRecordRepository.Received(1)
            .AddAsync(Arg.Any<BibRecord>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldUpdateCoverImage_WhenCoverImageIdIsProvided()
    {
        // Arrange
        BookQuickAddCommand command = CreateValidCommand() with { CoverImageId = "cover-123" };

        BibRecord mockBibRecord = CreateMockBibRecord(command);
        BibItem mockBibItem = CreateMockBibItem();

        _mockBibRecordRepository
            .AddAsync(Arg.Any<BibRecord>(), Arg.Any<CancellationToken>())
            .Returns(mockBibRecord);

        _mockBibItemRepository
            .ExistsAsync(Arg.Any<Expression<Func<BibItem, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(false);

        _mockBibItemRepository
            .AddAsync(Arg.Any<BibItem>(), Arg.Any<CancellationToken>())
            .Returns(mockBibItem);

        // Act
        KnResult<BibRecord> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        await _mockBibRecordRepository.Received(1)
            .AddAsync(Arg.Any<BibRecord>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldNotUpdateCoverImage_WhenCoverImageIdIsNull()
    {
        // Arrange
        BookQuickAddCommand command = CreateValidCommand() with { CoverImageId = null };

        BibRecord mockBibRecord = CreateMockBibRecord(command);
        BibItem mockBibItem = CreateMockBibItem();

        _mockBibRecordRepository
            .AddAsync(Arg.Any<BibRecord>(), Arg.Any<CancellationToken>())
            .Returns(mockBibRecord);

        _mockBibItemRepository
            .ExistsAsync(Arg.Any<Expression<Func<BibItem, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(false);

        _mockBibItemRepository
            .AddAsync(Arg.Any<BibItem>(), Arg.Any<CancellationToken>())
            .Returns(mockBibItem);

        // Act
        KnResult<BibRecord> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        await _mockBibRecordRepository.Received(1)
            .AddAsync(Arg.Any<BibRecord>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("\t")]
    public async Task Handle_ShouldNotUpdateCoverImage_WhenCoverImageIdIsWhitespace(string coverImageId)
    {
        // Arrange
        BookQuickAddCommand command = CreateValidCommand() with { CoverImageId = coverImageId };

        BibRecord mockBibRecord = CreateMockBibRecord(command);
        BibItem mockBibItem = CreateMockBibItem();

        _mockBibRecordRepository
            .AddAsync(Arg.Any<BibRecord>(), Arg.Any<CancellationToken>())
            .Returns(mockBibRecord);

        _mockBibItemRepository
            .ExistsAsync(Arg.Any<Expression<Func<BibItem, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(false);

        _mockBibItemRepository
            .AddAsync(Arg.Any<BibItem>(), Arg.Any<CancellationToken>())
            .Returns(mockBibItem);

        // Act
        KnResult<BibRecord> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        await _mockBibRecordRepository.Received(1)
            .AddAsync(Arg.Any<BibRecord>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldUpdateEdition_WhenEditionIsProvided()
    {
        // Arrange
        BookQuickAddCommand command = CreateValidCommand() with { Edition = "2nd Edition" };

        BibRecord mockBibRecord = CreateMockBibRecord(command);
        BibItem mockBibItem = CreateMockBibItem();

        _mockBibRecordRepository
            .AddAsync(Arg.Any<BibRecord>(), Arg.Any<CancellationToken>())
            .Returns(mockBibRecord);

        _mockBibItemRepository
            .ExistsAsync(Arg.Any<Expression<Func<BibItem, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(false);

        _mockBibItemRepository
            .AddAsync(Arg.Any<BibItem>(), Arg.Any<CancellationToken>())
            .Returns(mockBibItem);

        // Act
        KnResult<BibRecord> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        await _mockBibRecordRepository.Received(1)
            .AddAsync(Arg.Any<BibRecord>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ShouldUpdateNote_WhenNoteIsProvided()
    {
        // Arrange
        BookQuickAddCommand command = CreateValidCommand() with { Note = "Special handling required" };

        BibRecord mockBibRecord = CreateMockBibRecord(command);
        BibItem mockBibItem = CreateMockBibItem();

        _mockBibRecordRepository
            .AddAsync(Arg.Any<BibRecord>(), Arg.Any<CancellationToken>())
            .Returns(mockBibRecord);

        _mockBibItemRepository
            .ExistsAsync(Arg.Any<Expression<Func<BibItem, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(false);

        _mockBibItemRepository
            .AddAsync(Arg.Any<BibItem>(), Arg.Any<CancellationToken>())
            .Returns(mockBibItem);

        // Act
        KnResult<BibRecord> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        await _mockBibRecordRepository.Received(1)
            .AddAsync(Arg.Any<BibRecord>(), Arg.Any<CancellationToken>());
    }

    [Theory]
    [InlineData(null, null, null)]
    [InlineData("", "", "")]
    [InlineData(" ", " ", " ")]
    public async Task Handle_ShouldNotUpdateOptionalFields_WhenAllAreNullOrWhitespace(
        string? coverImageId, string? edition, string? note)
    {
        // Arrange
        BookQuickAddCommand command = CreateValidCommand() with
        {
            CoverImageId = coverImageId,
            Edition = edition,
            Note = note
        };

        BibRecord mockBibRecord = CreateMockBibRecord(command);
        BibItem mockBibItem = CreateMockBibItem();

        _mockBibRecordRepository
            .AddAsync(Arg.Any<BibRecord>(), Arg.Any<CancellationToken>())
            .Returns(mockBibRecord);

        _mockBibItemRepository
            .ExistsAsync(Arg.Any<Expression<Func<BibItem, bool>>>(), Arg.Any<CancellationToken>())
            .Returns(false);

        _mockBibItemRepository
            .AddAsync(Arg.Any<BibItem>(), Arg.Any<CancellationToken>())
            .Returns(mockBibItem);

        // Act
        KnResult<BibRecord> result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        await _mockBibRecordRepository.Received(1)
            .AddAsync(Arg.Any<BibRecord>(), Arg.Any<CancellationToken>());
    }

    private BookQuickAddCommand CreateValidCommand()
    {
        return new BookQuickAddCommand(_faker.Lorem.Sentence(),
            _faker.Name.FullName(),
            1,
            _faker.Random.String2(13, "0123456789"),
            _faker.Company.CompanyName(),
            _faker.Random.Int(1900, 2024),
            "eng",
            _faker.Random.Long(50, 1000),
            "1st");
    }

    private static BibRecord CreateMockBibRecord(BookQuickAddCommand command)
    {
        return BibRecord.CreateBookRecord(
            command.Title, command.Author, command.Isbn,
            command.Publisher, command.YearOfPublication,
            command.Language, command.NumberOfPages).Value;
    }

    private static BibItem CreateMockBibItem()
    {
        return BibItem.Create(
            bibRecordId: Guid.NewGuid().ToString(),
            barcode: "KB12345678",
            callNumber: "ABC DEF",
            location: "General Collection",
            itemType: ItemType.Book,
            status: ItemStatus.Available,
            notes: "Test item",
            acquisitionType: AcquisitionType.Purchase,
            acquisitionDate: DateOnly.FromDateTime(DateTime.UtcNow)).Value;
    }
}