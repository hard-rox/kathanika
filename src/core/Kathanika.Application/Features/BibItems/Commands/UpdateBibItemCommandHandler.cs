using Kathanika.Domain.Aggregates.BibItemAggregate;

namespace Kathanika.Application.Features.BibItems.Commands;

public sealed class UpdateBibItemCommandHandler(IBibItemRepository bibItemRepository)
    : ICommandHandler<UpdateBibItemCommand, KnResult<BibItem>>
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
            itemType: request.ItemType);

        if (!updateResult.IsSuccess)
        {
            return KnResult.Failure<BibItem>(updateResult.Errors);
        }

        if (request.ConditionNote is not null)
            bibItem.UpdateCondition(request.ConditionNote);

        if (request.Notes is not null)
            bibItem.UpdateNotes(request.Notes);

        await bibItemRepository.UpdateAsync(bibItem, cancellationToken);

        return KnResult.Success(bibItem);
    }
}