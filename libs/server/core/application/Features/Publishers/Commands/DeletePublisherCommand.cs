namespace Kathanika.Core.Application.Features.Publishers.Commands;

public sealed record DeletePublisherCommand(string Id) : IRequest<Result>;
