namespace Kathanika.Core.Domain.Primitives;

public sealed record KnError
{
    public string Code { get; private init; }
    public string? Description { get; private init; }
    public string? Message { get; private init; }
    internal KnError(
        string code,
        string? description = null,
        string? message = null
    )
    {
        Code = code;
        Description = description;
        Message = message;
    }

    public static implicit operator Result(KnError error) => Result.Failure(error);

    public static KnError ValidationError(string propertyName, string? message = null)
    {
        return new KnError("Kathanika.InvalidField", $"{propertyName} is invalid", message);
    }
}
