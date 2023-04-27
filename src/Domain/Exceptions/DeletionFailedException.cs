using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Exceptions;

public sealed class DeletionFailedException : DomainException
{
    public string ObjectName { get; init; }
    public string Reason { get; init; }
    public DeletionFailedException(Type objectType, string? reason = null)
        : base($"Cann't delete {objectType.Name}")
    {
        ObjectName = objectType.Name;
        Reason = reason ?? string.Empty;
    }
}
