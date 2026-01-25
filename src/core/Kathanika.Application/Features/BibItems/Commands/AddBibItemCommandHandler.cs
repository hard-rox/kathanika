using Kathanika.Domain.Aggregates.BibItemAggregate;

namespace Kathanika.Application.Features.BibItems.Commands;

public sealed class AddBibItemCommandHandler(IBibItemRepository bibItemRepository)
    : ICommandHandler<AddBibItemCommand, KnResult<BibItem>>
{
    public async Task<KnResult<BibItem>> Handle(AddBibItemCommand request, CancellationToken cancellationToken)
    {
        KnResult<BibItem> bibItemResult = BibItem.Create(
            request.BibRecordId,
            request.Barcode,
            request.CallNumber,
            request.Location,
            request.ItemType);

        if (bibItemResult.IsFailure)
        {
            return bibItemResult;
        }

        if (request.ConditionNote is not null)
            bibItemResult.Value.UpdateCondition(request.ConditionNote);

        if (request.Notes is not null)
            bibItemResult.Value.UpdateNotes(request.Notes);

        await bibItemRepository.AddAsync(bibItemResult.Value, cancellationToken);

        return KnResult.Success(bibItemResult.Value);
    }
}