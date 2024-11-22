using Kathanika.Domain.Aggregates.PatronAggregate;

namespace Kathanika.Application.Features.Patrons.Queries;

public sealed record GetPatronsQuery : IRequest<IQueryable<Patron>>;