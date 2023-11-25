using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Exceptions;

public sealed class InvalidFieldException(string fieldName, string? message = null) : DomainException(message ?? $"{fieldName} is invalid")
{
    public string FieldName { get; init; } = fieldName;
}
