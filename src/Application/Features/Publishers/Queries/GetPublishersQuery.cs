namespace Kathanika.Application.Features.Publishers.Queries;

public sealed record GetPublishersQuery : IRequest<IQueryable<Publisher>>;
