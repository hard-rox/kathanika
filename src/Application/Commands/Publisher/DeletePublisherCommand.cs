namespace Kathanika.Application.Commands;

public sealed record DeletePublisherCommand(string Id) : IRequest;
