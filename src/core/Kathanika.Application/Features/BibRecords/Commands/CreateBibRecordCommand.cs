using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Application.Features.BibRecords.Commands;

public sealed record CreateBibRecordCommand(
    string Leader,
    string ControlNumber,
    string ControlNumberIdentifier,
    DateTime DateTimeOfLatestTransaction,
    string FixedLengthDataElements,
    string? InternationalStandardBookNumber,
    string? InternationalStandardSerialNumber,
    CatalogingSource CatalogingSource
) : IRequest<KnResult<BibRecord>>;