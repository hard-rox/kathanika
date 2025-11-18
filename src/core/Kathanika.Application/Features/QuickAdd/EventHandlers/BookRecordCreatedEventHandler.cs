using Kathanika.Domain.Aggregates.BibItemAggregate;
using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Application.Features.QuickAdd.EventHandlers;

internal sealed class BookRecordCreatedEventHandler(
    IBibItemRepository bibItemRepository,
    ILogger<BookRecordCreatedEventHandler> logger)
    : INotificationHandler<BookRecordCreatedEvent>
{
    public async Task Handle(BookRecordCreatedEvent notification, CancellationToken cancellationToken)
    {
        if (notification.NumberOfCopies is null or 0) return;

        List<KnError> errors = [];
        List<BibItem> items = [];
        for (var i = 1; i <= notification.NumberOfCopies; i++)
        {
            //TODO: Generate barcode from another source
            var barcode = $"BK-{DateTime.Today.Year}-MAIN-{i:0000}";
            var callNumber = $"DM {i:0000}";
            KnResult<BibItem> itemResult = BibItem.Create(
                notification.BibRecordId,
                barcode,
                callNumber,
                "Main Library",
                ItemType.Book);

            if (itemResult.IsFailure)
            {
                errors.AddRange(itemResult.Errors);
                continue;
            }

            items.Add(itemResult.Value);
        }

        if (errors.Count != 0 || items.Count < notification.NumberOfCopies)
        {
            //TODO: Find way to feedback to user
            logger.LogError("Failed to create book items");
        }

        await bibItemRepository.AddAsync(items, cancellationToken);
    }
}