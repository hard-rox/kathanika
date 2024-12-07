using Kathanika.Domain.Aggregates.PatronAggregate;
using Kathanika.Domain.Primitives;

namespace Kathanika.Application.Features.Patrons.Queries;

internal sealed class GetPatronByIdQueryHandler(IPatronRepository patronRepository)
    : IRequestHandler<GetPatronByIdQuery, KnResult<Patron>>
{
    public async Task<KnResult<Patron>> Handle(GetPatronByIdQuery request, CancellationToken cancellationToken)
    {
        Patron? patron = await patronRepository.GetByIdAsync(request.Id, cancellationToken);
        return patron is null
            ? KnResult.Failure<Patron>(PatronAggregateErrors.NotFound(request.Id))
            : KnResult.Success(patron);
    }
}