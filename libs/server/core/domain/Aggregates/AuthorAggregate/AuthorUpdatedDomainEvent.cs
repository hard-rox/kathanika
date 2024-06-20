using Kathanika.Core.Domain.Primitives;

namespace Kathanika.Core.Domain.Aggregates.AuthorAggregate;

public sealed record AuthorUpdatedDomainEvent(string AuthorId) : IDomainEvent;
