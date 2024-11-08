using Kathanika.Application.Services;
using Kathanika.Domain.DomainEvents;

namespace Kathanika.Application.EventHandlers;

internal sealed class FileUsedDomainEventHandler(
    ILogger<FileUsedDomainEventHandler> logger,
    IFileStore fileStorageService
) : INotificationHandler<FileUsedDomainEvent>
{
    public async Task Handle(FileUsedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling {@DomainEvent} with {@EventData}", nameof(FileUsedDomainEventHandler), notification);

        await fileStorageService.MoveToStoreAsync(notification.FileId, cancellationToken);

        logger.LogInformation("Handled {@DomainEvent} with {@EventData}", nameof(FileUsedDomainEventHandler), notification);
    }
}
