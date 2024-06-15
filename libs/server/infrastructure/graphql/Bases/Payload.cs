namespace Kathanika.Infrastructure.Graphql.Bases;

public abstract class Payload(string message)
{
    public string Message { get; init; } = message;
}

public abstract class Payload<T>(string message, T data) : Payload(message)
{
    public T Data { get; init; } = data;
}
