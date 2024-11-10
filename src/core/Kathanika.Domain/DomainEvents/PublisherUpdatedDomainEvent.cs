using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.DomainEvents;

public sealed record PublisherUpdatedDomainEvent(string PublisherId) : IDomainEvent;
