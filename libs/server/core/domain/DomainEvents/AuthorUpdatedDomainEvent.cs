using Kathanika.Core.Domain.Primitives;

namespace Kathanika.Core.Domain.DomainEvents;

public sealed record AuthorUpdatedDomainEvent(string AuthorId) : IDomainEvent;
