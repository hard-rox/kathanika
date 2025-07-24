using Kathanika.Domain.Aggregates.BibItemAggregate;

namespace Kathanika.Application.Features.BibItems.Commands;

public sealed class CheckOutBibItemCommandHandler(IBibItemRepository bibItemRepository)
    : IRequestHandler<CheckOutBibItemCommand, KnResult<BibItem>>
{
    public async Task<KnResult<BibItem>> Handle(CheckOutBibItemCommand request, CancellationToken cancellationToken)
    {
        BibItem? bibItem = await bibItemRepository.GetByIdAsync(request.Id, cancellationToken);
        if (bibItem is null)
        {
            return KnResult.Failure<BibItem>(BibItemAggregateErrors.NotFound);
        }

        bibItem.CheckOut();
        await bibItemRepository.UpdateAsync(bibItem, cancellationToken);
        return KnResult.Success(bibItem);
    }
}