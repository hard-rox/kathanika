namespace Kathanika.Domain.Primitives;

public abstract class AggregateRoot : Entity
{
    private List<IDomainEvent> _domainEvents = [];

    public string CreatedByUserId { get; private init; } = string.Empty;
    public string CreatedByUserName { get; private init; } = string.Empty;
    public DateTimeOffset CreatedAt { get; private init; } = DateTimeOffset.MinValue;
    public string? LastModifiedByUserId { get; private set; }
    public string? LastModifiedByUserName { get; private set; }
    public DateTimeOffset? LastModifiedAt { get; private set; }

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents ??= [];
        _domainEvents.Add(domainEvent);
    }

    public List<IDomainEvent> GetDomainEvents()
    {
        return _domainEvents ?? [];
    }

    public void ClearDomainEvents()
    {
        _domainEvents ??= [];
        _domainEvents.Clear();
    }
}