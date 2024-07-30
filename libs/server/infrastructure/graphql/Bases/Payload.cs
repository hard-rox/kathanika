namespace Kathanika.Infrastructure.Graphql.Bases;

public abstract record Payload(string Message);

public abstract record Payload<TData>(string Message, TData? Data) : Payload(Message);
