namespace Kathanika.Application.Features.Publishers.Queries;

public sealed record GetPublisherByIdQuery(string Id) : IRequest<Publisher>;
