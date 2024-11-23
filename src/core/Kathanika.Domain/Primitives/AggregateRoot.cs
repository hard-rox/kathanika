using HotChocolate;

namespace Kathanika.Domain.Primitives;

public abstract class AggregateRoot : Entity
{
    private readonly List<IDomainEvent> _domainEvents = [];

    [GraphQLIgnore] public string CreatedByUserId { get; private init; } = string.Empty;
    [GraphQLIgnore] public string CreatedByUserName { get; private init; } = string.Empty;
    [GraphQLIgnore] public DateTimeOffset CreatedAt { get; private init; } = DateTimeOffset.MinValue;

    [GraphQLIgnore] public string? LastModifiedByUserId { get; private set; } = null;
    [GraphQLIgnore] public string? LastModifiedByUserName { get; private set; } = null;
    [GraphQLIgnore] public DateTimeOffset? LastModifiedAt { get; private set; } = null;

    // ReSharper disable once NullCoalescingConditionIsAlwaysNotNullAccordingToAPIContract
    [GraphQLIgnore] public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents ?? [];

    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    [GraphQLIgnore]
    public void ClearDomainEvents()
    {
        // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
        _domainEvents?.Clear();
    }
}