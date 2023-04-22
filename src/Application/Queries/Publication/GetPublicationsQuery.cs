namespace Kathanika.Application.Queries;

public sealed record GetPublicationsQuery : IRequest<IQueryable<Publication>>;
