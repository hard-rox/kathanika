using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.DomainEvents;

public sealed record FileUsedDomainEvent(
    string FileId
) : IDomainEvent;
