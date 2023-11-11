namespace Kathanika.Application.Features.Publishers.Commands;

public sealed record DeletePublisherCommand(string Id) : IRequest;
