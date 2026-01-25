using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Application.Features.QuickAdd.Commands;

internal sealed class BookQuickAddCommandHandler(
    IBibRecordRepository bibRecordRepository)
    : ICommandHandler<BookQuickAddCommand, KnResult<BibRecord>>
{
    public async Task<KnResult<BibRecord>> Handle(BookQuickAddCommand request, CancellationToken cancellationToken)
    {
        KnResult<BibRecord> bibRecordResult = BibRecord.CreateBookRecord(
            request.Title,
            request.Author,
            request.Isbn,
            request.Publisher,
            request.YearOfPublication,
            request.Language,
            request.NumberOfPages,
            request.NumberOfCopies
        );

        if (bibRecordResult.IsFailure)
            return bibRecordResult;

        if (!string.IsNullOrWhiteSpace(request.CoverImageId))
        {
            KnResult updateResult = bibRecordResult.Value.UpdateCoverImage(request.CoverImageId);
            if (updateResult.IsFailure)
                return KnResult<BibRecord>.Failure(updateResult.Errors);
        }

        if (!string.IsNullOrWhiteSpace(request.Edition))
        {
            KnResult updateResult = bibRecordResult.Value.UpdateEdition(request.Edition);
            if (updateResult.IsFailure)
                return KnResult<BibRecord>.Failure(updateResult.Errors);
        }

        if (!string.IsNullOrWhiteSpace(request.Note))
        {
            KnResult updateResult = bibRecordResult.Value.UpdateNote(request.Note);
            if (updateResult.IsFailure)
                return KnResult<BibRecord>.Failure(updateResult.Errors);
        }

        BibRecord createdBibRecord = await bibRecordRepository.AddAsync(bibRecordResult.Value, cancellationToken);

        return KnResult.Success(createdBibRecord);
    }
}