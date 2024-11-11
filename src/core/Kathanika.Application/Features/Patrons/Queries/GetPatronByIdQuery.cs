using Kathanika.Domain.Aggregates.PatronAggregate;
using Kathanika.Domain.Primitives;

namespace Kathanika.Application.Features.Patrons.Queries;
public sealed record GetPatronByIdQuery(
    string Id
    ) : IRequest<Result<Patron>>;