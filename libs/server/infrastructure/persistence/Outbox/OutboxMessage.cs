using Kathanika.Core.Domain.Primitives;

namespace Kathanika.Infrastructure.Persistence.Outbox;

public class OutboxMessage(IDomainEvent domainEvent)
{
    public string Id { get; init; } = string.Empty;
    public IDomainEvent DomainEvent { get; init; } = domainEvent;
    public DateTimeOffset OccurredAt { get; private init; } = DateTimeOffset.Now;
    public DateTimeOffset? ProcessedAt { get; set; }
    public int ProcessAttempt { get; init; } = 0;
    public string? LastOccurredError { get; set; }
}
