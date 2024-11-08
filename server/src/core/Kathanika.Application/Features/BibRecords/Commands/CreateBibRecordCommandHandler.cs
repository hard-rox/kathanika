using Kathanika.Domain.Aggregates.BibRecordAggregate;
using Kathanika.Domain.Primitives;

namespace Kathanika.Application.Features.BibRecords.Commands;

internal sealed class CreateBibRecordCommandHandler(IBibRecordRepository bibRecordRepository)
    : IRequestHandler<CreateBibRecordCommand, Result<BibRecord>>
{
    public async Task<Result<BibRecord>> Handle(CreateBibRecordCommand request, CancellationToken cancellationToken)
    {
        Result<BibRecord> bibRecordResult = BibRecord.Create(
            request.Leader,
            request.ControlNumber,
            request.ControlNumberIdentifier,
            request.DateTimeOfLatestTransaction,
            request.FixedLengthDataElements,
            request.CatalogingSource
        );

        if (bibRecordResult.IsFailure)
            return bibRecordResult;

        BibRecord createdBibRecord = await bibRecordRepository.AddAsync(bibRecordResult.Value, cancellationToken);

        return Result.Success(createdBibRecord);
    }
}
