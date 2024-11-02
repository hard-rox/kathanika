using Kathanika.Core.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Core.Application.Features.BibRecords.Commands;

public sealed record CreateBibRecordCommand(
    string Leader,
    string ControlNumber,
    string ControlNumberIdentifier,
    DateTime DateTimeOfLatestTransaction,
    string FixedLengthDataElements,
    CatalogingSource CatalogingSource
) : IRequest<Result<BibRecord>>;
