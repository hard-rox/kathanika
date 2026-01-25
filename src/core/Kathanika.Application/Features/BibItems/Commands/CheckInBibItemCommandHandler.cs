using Kathanika.Domain.Aggregates.BibItemAggregate;

namespace Kathanika.Application.Features.BibItems.Commands;

public sealed class CheckInBibItemCommandHandler(IBibItemRepository bibItemRepository)
    : ICommandHandler<CheckInBibItemCommand, KnResult<BibItem>>
{
    public async Task<KnResult<BibItem>> Handle(CheckInBibItemCommand request, CancellationToken cancellationToken)
    {
        BibItem? bibItem = await bibItemRepository.GetByIdAsync(request.Id, cancellationToken);
        if (bibItem is null)
        {
            return KnResult.Failure<BibItem>(BibItemAggregateErrors.NotFound);
        }

        KnResult checkInResult = bibItem.CheckIn();
        if (!checkInResult.IsSuccess)
        {
            return KnResult.Failure<BibItem>(checkInResult.Errors);
        }

        await bibItemRepository.UpdateAsync(bibItem, cancellationToken);
        return KnResult.Success(bibItem);
    }
}