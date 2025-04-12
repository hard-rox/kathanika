using Kathanika.Domain.Aggregates.PatronAggregate;

namespace Kathanika.Application.Features.Patrons.Queries;

public sealed record GetPatronByIdQuery(
    string Id
) : IRequest<KnResult<Patron>>;