using Kathanika.Application.Services;
using Kathanika.Domain.DomainEvents;

namespace Kathanika.Application.EventHandlers;

internal sealed class FileReplacedDomainEventHandler(
    ILogger<FileReplacedDomainEventHandler> logger,
    IFileStore fileStorageService
) : INotificationHandler<FileReplacedDomainEvent>
{
    public async Task Handle(FileReplacedDomainEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling domain event {@DomainEvent}", nameof(FileReplacedDomainEvent));
        await fileStorageService.MoveToStoreAsync(notification.NewFileId, cancellationToken);
        await fileStorageService.RemoveFromStoreAsync(notification.OldFileId, cancellationToken);
        logger.LogInformation("Handled domain event {@DomainEvent}", nameof(FileReplacedDomainEvent));
    }
}
