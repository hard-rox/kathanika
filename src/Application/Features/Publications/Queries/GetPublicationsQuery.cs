namespace Kathanika.Application.Features.Publications.Queries;

public sealed record GetPublicationsQuery : IRequest<IQueryable<Publication>>;
