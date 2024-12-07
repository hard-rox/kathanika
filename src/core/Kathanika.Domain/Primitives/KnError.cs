namespace Kathanika.Domain.Primitives;

public record KnError
{
    internal KnError(
        string code,
        string message,
        string? description = null
    )
    {
        Code = code;
        Description = description;
        Message = message;
    }

    public string Code { get; private init; }
    public string? Description { get; private init; }
    public string Message { get; private init; }

    public static implicit operator KnResult(KnError error)
    {
        return KnResult.Failure(error);
    }

    public static KnError ValidationError(string fieldName, string message)
    {
        return new ValidationError(fieldName, message);
    }
}

public sealed record ValidationError : KnError
{
    internal ValidationError(
        string fieldName,
        string message
    ) : base(
        "Kathanika.ValidationError",
        message,
        $"{fieldName} is invalid"
    )
    {
        FieldName = fieldName;
    }

    public string FieldName { get; private init; }
}