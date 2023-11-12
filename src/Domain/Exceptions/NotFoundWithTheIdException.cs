using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Exceptions;

public sealed class NotFoundWithTheIdException : DomainException
{
    public string ObjectName { get; init; }
    public string Id { get; init; }

    public NotFoundWithTheIdException(Type objectType, string id, string? message = null)
        : base(message ?? $"No {objectType.Name} found with this Id: \"{id}\"")
    {
        Id = id;
        ObjectName = objectType.Name;
    }
}
