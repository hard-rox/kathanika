namespace Kathanika.Domain.Primitives;

public record KnError
{
    public string Code { get; private init; }
    public string? Description { get; private init; }
    public string Message { get; private init; }
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

    public static implicit operator Result(KnError error) => Result.Failure(error);

    public static KnError ValidationError(string fieldName, string message)
    {
        return new ValidationError(fieldName, message);
    }
}

public sealed record ValidationError : KnError
{
    public string FieldName { get; private init; }
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
}
