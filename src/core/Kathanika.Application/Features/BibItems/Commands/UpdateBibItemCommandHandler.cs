using Kathanika.Domain.Aggregates.BibItemAggregate;
using Kathanika.Domain.Aggregates.VendorAggregate;

namespace Kathanika.Application.Features.BibItems.Commands;

public sealed class UpdateBibItemCommandHandler(IBibItemRepository bibItemRepository, IVendorRepository vendorRepository)
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

        // Update status using the new UpdateStatus method with proper business rules
        if (request.Status != bibItem.Status)
        {
            KnResult statusUpdateResult = bibItem.UpdateStatus(request.Status);
            if (!statusUpdateResult.IsSuccess)
            {
                return KnResult.Failure<BibItem>(statusUpdateResult.Errors);
            }
        }

        await bibItemRepository.UpdateAsync(bibItem, cancellationToken);

        return KnResult.Success(bibItem);
    }
}
