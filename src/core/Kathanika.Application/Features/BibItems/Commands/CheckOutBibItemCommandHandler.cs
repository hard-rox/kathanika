using Kathanika.Domain.Aggregates.BibItemAggregate;

namespace Kathanika.Application.Features.BibItems.Commands;

public sealed class CheckOutBibItemCommandHandler(IBibItemRepository bibItemRepository)
    : ICommandHandler<CheckOutBibItemCommand, KnResult<BibItem>>
{
    public async Task<KnResult<BibItem>> Handle(CheckOutBibItemCommand request, CancellationToken cancellationToken)
    {
        BibItem? bibItem = await bibItemRepository.GetByIdAsync(request.Id, cancellationToken);
        if (bibItem is null)
        {
            return KnResult.Failure<BibItem>(BibItemAggregateErrors.NotFound);
        }

        KnResult result = bibItem.CheckOut();
        if (result.IsFailure)
        {
            return KnResult.Failure<BibItem>(result.Errors);
        }

        await bibItemRepository.UpdateAsync(bibItem, cancellationToken);
        return KnResult.Success(bibItem);
    }
}