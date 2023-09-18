namespace Kathanika.Application.Publishers.Commands;

public sealed record DeletePublisherCommand(string Id) : IRequest;
