using Kathanika.Application.EventHandlers;
using Kathanika.Application.Services;
using Kathanika.Domain.DomainEvents;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Kathanika.Application.Tests.EventHandlers;

public class FileUsedDomainEventHandlerTests
{
    [Fact]
    public async Task Handle_Should_CallFileMoveAsync()
    {
        ILogger<FileUsedDomainEventHandler> logger = new NullLogger<FileUsedDomainEventHandler>();
        IFileStore fileStorageService = Substitute.For<IFileStore>();
        FileUsedDomainEventHandler handler = new(logger, fileStorageService);

        var fileId = Guid.NewGuid().ToString();
        FileUsedDomainEvent fileUsedEvent = new(fileId);

        await handler.Handle(fileUsedEvent, CancellationToken.None);

        await fileStorageService.Received(1)
            .MoveToStoreAsync(Arg.Is(fileId), Arg.Any<CancellationToken>());
    }
}