using Kathanika.Domain.Aggregates.BibItemAggregate;

namespace Kathanika.Application.Features.BibItems.Commands;

public sealed class AddBibItemCommandHandler(IBibItemRepository bibItemRepository)
    : IRequestHandler<AddBibItemCommand, KnResult<BibItem>>
{
    public async Task<KnResult<BibItem>> Handle(AddBibItemCommand request, CancellationToken cancellationToken)
    {
        KnResult<BibItem> bibItemResult = BibItem.Create(
            request.BibRecordId,
            request.Barcode,
            request.CallNumber,
            request.Location,
            request.ItemType,
            request.Status,
            request.ConditionNote,
            request.Notes);

        await bibItemRepository.AddAsync(bibItemResult.Value, cancellationToken);

        return KnResult.Success(bibItemResult.Value);
    }
}
