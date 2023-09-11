namespace Kathanika.Application.Publications.Queries;

public sealed record GetPublicationsQuery : IRequest<IQueryable<Publication>>;
