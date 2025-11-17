using Kathanika.Application.Features.QuickAdd.EventHandlers;
using Kathanika.Domain.Aggregates.BibItemAggregate;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Kathanika.Application.Tests.Features.QuickAdd.EventHandlers;

public class BookRecordCreatedEventHandlerTests
{
    private readonly IBibItemRepository _bibItemRepository;
    private readonly ILogger<BookRecordCreatedEventHandler> _logger;

    public BookRecordCreatedEventHandlerTests()
    {
        _bibItemRepository = Substitute.For<IBibItemRepository>();
        _logger = new NullLogger<BookRecordCreatedEventHandler>();
        new BookRecordCreatedEventHandler(_bibItemRepository, _logger);
    }
}