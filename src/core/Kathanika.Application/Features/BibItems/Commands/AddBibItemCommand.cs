using Kathanika.Domain.Aggregates.BibItemAggregate;

namespace Kathanika.Application.Features.BibItems.Commands;

public sealed record AddBibItemCommand(
    string BibRecordId,
    string Barcode,
    string CallNumber,
    string Location,
    ItemType ItemType,
    ItemStatus Status = ItemStatus.Available,
    string? ConditionNote = null,
    string? Notes = null
) : IRequest<KnResult<BibItem>>;