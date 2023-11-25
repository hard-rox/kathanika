using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Exceptions;

public sealed class NotFoundWithTheIdException(Type objectType, string id, string? message = null) : DomainException(message ?? $"No {objectType.Name} found with this Id: \"{id}\"")
{
    public string ObjectName { get; init; } = objectType.Name;
    public string Id { get; init; } = id;
}
