using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Exceptions;

public sealed class DeletionFailedException(Type objectType, string? reason = null) : DomainException($"Cann't delete {objectType.Name}")
{
    public string ObjectName { get; init; } = objectType.Name;
    public string Reason { get; init; } = reason ?? string.Empty;
}
