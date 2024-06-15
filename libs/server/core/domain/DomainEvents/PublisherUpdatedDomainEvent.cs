using Kathanika.Core.Domain.Primitives;

namespace Kathanika.Core.Domain.DomainEvents;

public sealed record PublisherUpdatedDomainEvent(string PublisherId) : IDomainEvent;
