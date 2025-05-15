using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Application.Features.BibRecords.Commands;

internal sealed class CreateBibRecordCommandHandler(IBibRecordRepository bibRecordRepository)
    : IRequestHandler<CreateBibRecordCommand, KnResult<BibRecord>>
{
    /// <summary>
    /// Handles the creation of a new bibliographic book record and adds it to the repository.
    /// </summary>
    /// <param name="request">The command containing book details for the new bibliographic record.</param>
    /// <param name="cancellationToken">Token for canceling the asynchronous operation.</param>
    /// <returns>A result containing the created <see cref="BibRecord"/> if successful, or a failure result otherwise.</returns>
    public async Task<KnResult<BibRecord>> Handle(CreateBibRecordCommand request, CancellationToken cancellationToken)
    {
        KnResult<BibRecord> bibRecordResult = BibRecord.CreateBookRecord(
            request.Title,
            request.Isbn,
            request.PersonalName,
            request.PublisherName,
            request.PublicationDate,
            request.Extent,
            request.Dimensions,
            request.CoverImageId
        );

        if (bibRecordResult.IsFailure)
            return bibRecordResult;

        BibRecord createdBibRecord = await bibRecordRepository.AddAsync(bibRecordResult.Value, cancellationToken);

        return createdBibRecord;
    }
}