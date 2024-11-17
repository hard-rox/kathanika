using HotChocolate;

namespace Kathanika.Domain.Primitives;

public abstract class AggregateRoot : Entity
{
    private List<IDomainEvent> _domainEvents = [];

    [GraphQLIgnore]
    public string CreatedByUserId { get; private init; } = string.Empty;
    [GraphQLIgnore]
    public string CreatedByUserName { get; private init; } = string.Empty;
    [GraphQLIgnore]
    public DateTimeOffset CreatedAt { get; private init; } = DateTimeOffset.MinValue;
    [GraphQLIgnore]
    public string? LastModifiedByUserId { get; private set; }
    [GraphQLIgnore]
    public string? LastModifiedByUserName { get; private set; }
    [GraphQLIgnore]
    public DateTimeOffset? LastModifiedAt { get; private set; }

    [GraphQLIgnore]
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents ??= [];
        _domainEvents.Add(domainEvent);
    }

    [GraphQLIgnore]
    public void ClearDomainEvents()
    {
        _domainEvents ??= [];
        _domainEvents.Clear();
    }
}