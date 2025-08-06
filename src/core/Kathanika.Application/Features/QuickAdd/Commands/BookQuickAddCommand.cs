using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Application.Features.QuickAdd.Commands;

using System.ComponentModel.DataAnnotations;

public sealed record BookQuickAddCommand(
    [Required] string Title,
    [Required] string Author,
    [Required] int NumberOfCopies,
    string? Isbn = null,
    string? Publisher = null,
    int? YearOfPublication = null,
    string? Language = null,
    long? NumberOfPages = null,
    string? Edition = null,
    string? Description = null,
    string? CoverImageId = null
) : IRequest<KnResult<BibRecord>>;