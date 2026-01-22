using MediatR;

namespace Kathanika.Application.Abstractions.Messaging;

/// <summary>
/// Represents a handler for a query.
/// </summary>
/// <typeparam name="TQuery">The type of query being handled.</typeparam>
/// <typeparam name="TResponse">The type of the response from the handler.</typeparam>
public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
{
}