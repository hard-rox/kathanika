using Kathanika.Core.Domain.Primitives;

namespace Kathanika.Core.Domain.Exceptions;

public sealed class InvalidFieldException(string fieldName, string? message = null) : DomainException(message ?? $"{fieldName} is invalid")
{
    public string FieldName { get; init; } = fieldName;
}
