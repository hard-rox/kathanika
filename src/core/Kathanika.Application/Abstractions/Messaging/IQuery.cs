using MediatR;

namespace Kathanika.Application.Abstractions.Messaging;

/// <summary>
/// Represents a query that can be executed.
/// </summary>
/// <typeparam name="TResponse">The type of the response returned by the query.</typeparam>
public interface IQuery<out TResponse> : IRequest<TResponse>
{
}