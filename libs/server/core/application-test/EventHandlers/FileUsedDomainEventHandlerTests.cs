using Kathanika.Core.Application.EventHandlers;
using Kathanika.Core.Application.Services;
using Kathanika.Core.Domain.DomainEvents;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Kathanika.Core.Application.Test.EventHandlers;

public class FileUsedDomainEventHandlerTests
{
    [Fact]
    public async Task Handle_Should_CallFileMoveAsync()
    {
        ILogger<FileUsedDomainEventHandler> logger = new NullLogger<FileUsedDomainEventHandler>();
        IFileStore fileStorageService = Substitute.For<IFileStore>();
        FileUsedDomainEventHandler handler = new(logger, fileStorageService);

        string fileId = Guid.NewGuid().ToString();
        FileUsedDomainEvent fileUsedEvent = new(fileId);

        await handler.Handle(fileUsedEvent, CancellationToken.None);

        await fileStorageService.Received(1)
            .MoveToStoreAsync(Arg.Is(fileId), Arg.Any<CancellationToken>());
    }
}
