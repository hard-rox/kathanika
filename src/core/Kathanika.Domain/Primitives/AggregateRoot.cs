using HotChocolate;

namespace Kathanika.Domain.Primitives;

public abstract class AggregateRoot : Entity
{
    [GraphQLIgnore] public string CreatedByUserId { get; private init; } = string.Empty;
    [GraphQLIgnore] public string CreatedByUserName { get; private init; } = string.Empty;
    [GraphQLIgnore] public DateTimeOffset CreatedAt { get; private init; } = DateTimeOffset.MinValue;

    [GraphQLIgnore] public string? LastModifiedByUserId { get; private set; } = null;
    [GraphQLIgnore] public string? LastModifiedByUserName { get; private set; } = null;
    [GraphQLIgnore] public DateTimeOffset? LastModifiedAt { get; private set; } = null;

    [GraphQLIgnore] 
    public IReadOnlyList<IDomainEvent> DomainEvents
    {
        get => field ?? [];
    } = new List<IDomainEvent>();

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        ((List<IDomainEvent>)DomainEvents).Add(domainEvent);
    }

    [GraphQLIgnore]
    public void ClearDomainEvents()
    {
        ((List<IDomainEvent>)DomainEvents).Clear();
    }
}