using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Application.Features.QuickAdd.Commands;

public sealed record BookQuickAddCommand(
    string Title,
    string Author,
    int NumberOfCopies,
    string Isbn,
    string Publisher,
    int YearOfPublication,
    string Language,
    long NumberOfPages,
    string Edition,
    string? Description = null,
    string? CoverImageId = null
) : IRequest<KnResult<BibRecord>>;