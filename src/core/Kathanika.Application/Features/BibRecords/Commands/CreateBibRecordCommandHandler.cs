using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Application.Features.BibRecords.Commands;

internal sealed class CreateBibRecordCommandHandler(IBibRecordRepository bibRecordRepository)
    : IRequestHandler<CreateBibRecordCommand, KnResult<BibRecord>>
{
    public async Task<KnResult<BibRecord>> Handle(CreateBibRecordCommand request, CancellationToken cancellationToken)
    {
        KnResult<BibRecord> bibRecordResult = BibRecord.Create(
            request.Title,
            request.Isbn,
            request.Author,
            request.PublisherName,
            request.PublicationDate,
            request.Extent,
            request.Dimensions
        );

        if (bibRecordResult.IsFailure)
            return bibRecordResult;

        BibRecord createdBibRecord = await bibRecordRepository.AddAsync(bibRecordResult.Value, cancellationToken);

        return createdBibRecord;
    }
}