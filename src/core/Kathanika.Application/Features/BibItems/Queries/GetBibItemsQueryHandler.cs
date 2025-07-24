using Kathanika.Domain.Aggregates.BibItemAggregate;

namespace Kathanika.Application.Features.BibItems.Queries;

public sealed class GetBibItemsQueryHandler(IBibItemRepository bibItemRepository)
    : IRequestHandler<GetBibItemsQuery, IQueryable<BibItem>>
{
    public async Task<IQueryable<BibItem>> Handle(GetBibItemsQuery request, CancellationToken cancellationToken)
    {
        IReadOnlyList<BibItem> bibItems = await bibItemRepository
            .ListAllAsync(x => x.BibRecordId == request.BibRecordId, cancellationToken);
        return bibItems.AsQueryable();
    }
}