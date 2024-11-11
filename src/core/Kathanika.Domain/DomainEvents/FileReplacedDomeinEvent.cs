using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.DomainEvents;

public sealed record FileReplacedDomainEvent(string OldFileId, string NewFileId) : IDomainEvent;