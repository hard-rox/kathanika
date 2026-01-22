using MediatR;

namespace Kathanika.Application.Abstractions.Messaging;

/// <summary>
/// MediatR implementation of the IDispatcher interface.
/// This class serves as an adapter between our custom CQRS interfaces and MediatR,
/// allowing easy replacement of MediatR in the future.
/// </summary>
internal sealed class MediatRDispatcher : IDispatcher
{
    private readonly IMediator _mediator;

    public MediatRDispatcher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public Task<TResponse> Send<TResponse>(ICommand<TResponse> command, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(command, cancellationToken);
    }

    public Task<TResponse> Send<TResponse>(IQuery<TResponse> query, CancellationToken cancellationToken = default)
    {
        return _mediator.Send(query, cancellationToken);
    }
}