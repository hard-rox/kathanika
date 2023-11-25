namespace Kathanika.Infrastructure.GraphQL.Bases;

public abstract class Payload
{
    public string Message { get; init; }

    protected Payload(string message)
    {
        Message = message;
    }
}

public abstract class Payload<T> : Payload
{
    public T Data { get; init; }
    protected Payload(string message, T data) : base(message)
    {
        Data = data;
    }
}
