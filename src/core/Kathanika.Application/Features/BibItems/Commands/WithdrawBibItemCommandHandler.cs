using Kathanika.Domain.Aggregates.BibItemAggregate;

namespace Kathanika.Application.Features.BibItems.Commands;

public sealed class WithdrawBibItemCommandHandler(IBibItemRepository bibItemRepository)
    : ICommandHandler<WithdrawBibItemCommand, KnResult<BibItem>>
{
    public async Task<KnResult<BibItem>> Handle(WithdrawBibItemCommand request, CancellationToken cancellationToken)
    {
        BibItem? bibItem = await bibItemRepository.GetByIdAsync(request.Id, cancellationToken);
        if (bibItem is null)
        {
            return KnResult.Failure<BibItem>(BibItemAggregateErrors.NotFound);
        }

        KnResult result = bibItem.Withdraw(request.Reason);
        if (result.IsFailure)
        {
            return KnResult.Failure<BibItem>(result.Errors);
        }

        await bibItemRepository.UpdateAsync(bibItem, cancellationToken);
        return KnResult.Success(bibItem);
    }
}