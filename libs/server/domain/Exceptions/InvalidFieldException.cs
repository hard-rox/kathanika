using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Exceptions;

public sealed class InvalidFieldException : DomainException
{
    public string FieldName { get; init; }
    public InvalidFieldException(string fieldName, string? message = null)
        : base(message ?? $"{fieldName} is invalid")
    {
        FieldName = fieldName;
    }
}
