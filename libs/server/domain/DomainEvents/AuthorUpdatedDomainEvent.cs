using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.DomainEvents;

public sealed record AuthorUpdatedDomainEvent(string AuthorId) : IDomainEvent;
