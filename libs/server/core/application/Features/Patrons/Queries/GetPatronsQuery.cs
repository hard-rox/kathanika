namespace Kathanika.Core.Application.Features.Patrons.Queries;
public sealed record GetPatronsQuery : IRequest<IQueryable<Patron>>;
