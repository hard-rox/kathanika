namespace Kathanika.Application.Queries;

public sealed record GetPublishersQuery : IRequest<IQueryable<Publisher>>;
