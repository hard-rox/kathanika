using Kathanika.Domain.Aggregates.PatronAggregate;

namespace Kathanika.Application.Features.Patrons.Queries;

public sealed record GetPatronsQuery : IQuery<IQueryable<Patron>>;