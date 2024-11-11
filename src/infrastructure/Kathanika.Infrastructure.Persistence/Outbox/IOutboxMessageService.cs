namespace Kathanika.Infrastructure.Persistence.Outbox;

public interface IOutboxMessageService
{
    Task<IReadOnlyList<OutboxMessage>> GetUnprocessedOutboxMessagesFromDb(int limit = 20, CancellationToken cancellationToken = default);
    Task SetOutboxMessageProcessed(string id);
    Task SetOutboxMessageErrors(string id, Exception exception);
}