using Kathanika.Domain.Aggregates.BibItemAggregate;

namespace Kathanika.Application.Features.BibItems.Queries;

public sealed class GetBibItemByIdQueryHandler(IBibItemRepository bibItemRepository)
    : IRequestHandler<GetBibItemByIdQuery, KnResult<BibItem>>
{
    public async Task<KnResult<BibItem>> Handle(GetBibItemByIdQuery request, CancellationToken cancellationToken)
    {
        BibItem? bibItem = await bibItemRepository.GetByIdAsync(request.Id, cancellationToken);

        return bibItem is null ?
            KnResult.Failure<BibItem>(BibItemAggregateErrors.NotFound)
            : KnResult.Success(bibItem);
    }
}