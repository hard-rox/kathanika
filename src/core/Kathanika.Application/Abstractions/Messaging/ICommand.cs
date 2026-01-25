using MediatR;

namespace Kathanika.Application.Abstractions.Messaging;

/// <summary>
/// Represents a command that can be executed.
/// </summary>
/// <typeparam name="TResponse">The type of the response returned by the command.</typeparam>
public interface ICommand<out TResponse> : IRequest<TResponse>
{
}