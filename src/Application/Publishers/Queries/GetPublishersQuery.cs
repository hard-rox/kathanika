namespace Kathanika.Application.Publishers.Queries;

public sealed record GetPublishersQuery : IRequest<IQueryable<Publisher>>;
