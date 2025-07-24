using Kathanika.Domain.Aggregates.BibItemAggregate;

namespace Kathanika.Application.Features.BibItems.Commands;

public sealed record UpdateBibItemCommand(
    string Id,
    string Barcode,
    string CallNumber,
    string Location,
    ItemType ItemType,
    ItemStatus Status,
    string? ConditionNote = null,
    string? Notes = null
) : IRequest<KnResult<BibItem>>;
