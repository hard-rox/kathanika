using Kathanika.Core.Application.Services;
using Kathanika.Core.Domain.DomainEvents;

internal sealed class FileReplacedDomainEventHandler(
    ILogger<FileReplacedDomainEventHandler> logger,
    IFileStore fileStorageService
) : INotificationHandler<FileReplacedDomainEvent>
{
    public async Task Handle(FileReplacedDomainEvent notification, CancellationToken cancellationToken)
    {
        await fileStorageService.MoveToStoreAsync(notification.NewFileId, cancellationToken);
        await fileStorageService.RemoveFromStoreAsync(notification.OldFileId, cancellationToken);
    }
}
