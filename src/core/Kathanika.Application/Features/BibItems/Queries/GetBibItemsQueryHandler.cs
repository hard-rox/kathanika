using Kathanika.Domain.Aggregates.BibItemAggregate;

namespace Kathanika.Application.Features.BibItems.Queries;

public sealed class GetBibItemsQueryHandler(IBibItemRepository bibItemRepository)
    : IQueryHandler<GetBibItemsQuery, IQueryable<BibItem>>
{
    public async Task<IQueryable<BibItem>> Handle(GetBibItemsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<BibItem> query = bibItemRepository
            .AsQueryable()
            .Where(x => x.BibRecordId == request.BibRecordId);
        return await Task.FromResult(query);
    }
}