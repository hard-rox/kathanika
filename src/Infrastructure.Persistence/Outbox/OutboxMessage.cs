namespace Kathanika.Infrastructure.Persistence.Outbox;

public class OutboxMessage
{
    public string Id { get; init; } = string.Empty;
    public string Type { get; init; } = string.Empty;
    public string Content { get; init; } = string.Empty;
    public DateTimeOffset OccurredAt { get; set; }
    public DateTimeOffset? ProcessedAt { get; set; }
    public int ProcessAttempt { get; init; } = 0;
    public string? LastOccurredError { get; init; }
}