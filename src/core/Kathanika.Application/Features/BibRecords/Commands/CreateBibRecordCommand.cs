using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Application.Features.BibRecords.Commands;

public sealed record CreateBibRecordCommand(
    string Title,
    string? Isbn,
    string? Author,
    string? PublisherName,
    string? PublicationDate,
    string? Extent,
    string? Dimensions,
    string? CoverImageId
) : IRequest<KnResult<BibRecord>>;