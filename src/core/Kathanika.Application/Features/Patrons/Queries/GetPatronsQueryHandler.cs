using Kathanika.Domain.Aggregates.PatronAggregate;

namespace Kathanika.Application.Features.Patrons.Queries;
internal sealed class GetPatronsQueryHandler(IPatronRepository patronRepository)
    : IRequestHandler<GetPatronsQuery, IQueryable<Patron>>
{
    public async Task<IQueryable<Patron>> Handle(GetPatronsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Patron> patronQuery = await Task.Run(() => patronRepository.AsQueryable(), cancellationToken);
        return patronQuery;
    }
}
