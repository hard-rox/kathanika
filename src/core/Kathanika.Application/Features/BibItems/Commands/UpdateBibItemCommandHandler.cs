using Kathanika.Domain.Aggregates.BibItemAggregate;

namespace Kathanika.Application.Features.BibItems.Commands;

public sealed class UpdateBibItemCommandHandler(IBibItemRepository bibItemRepository)
    : IRequestHandler<UpdateBibItemCommand, KnResult<BibItem>>
{
    public async Task<KnResult<BibItem>> Handle(UpdateBibItemCommand request, CancellationToken cancellationToken)
    {
        BibItem? bibItem = await bibItemRepository.GetByIdAsync(request.Id, cancellationToken);
        if (bibItem is null)
        {
            return KnResult.Failure<BibItem>(BibItemAggregateErrors.NotFound);
        }

        KnResult updateResult = bibItem.Update(
            barcode: request.Barcode,
            callNumber: request.CallNumber,
            location: request.Location,
            itemType: request.ItemType,
            conditionNote: request.ConditionNote,
            notes: request.Notes);

        if (!updateResult.IsSuccess)
        {
            return KnResult.Failure<BibItem>(updateResult.Errors);
        }

        await bibItemRepository.UpdateAsync(bibItem, cancellationToken);

        return KnResult.Success(bibItem);
    }
}