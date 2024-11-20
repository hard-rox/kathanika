using Kathanika.Domain.Aggregates.PatronAggregate;
using Kathanika.Domain.Primitives;

namespace Kathanika.Application.Features.Patrons.Queries;

internal sealed class GetPatronByIdQueryHandler(IPatronRepository patronRepository)
    : IRequestHandler<GetPatronByIdQuery, Result<Patron>>
{
    public async Task<Result<Patron>> Handle(GetPatronByIdQuery request, CancellationToken cancellationToken)
    {
        Patron? patron = await patronRepository.GetByIdAsync(request.Id, cancellationToken);
        return patron is null
            ? Result.Failure<Patron>(PatronAggregateErrors.NotFound(request.Id))
            : Result.Success(patron);
    }
}