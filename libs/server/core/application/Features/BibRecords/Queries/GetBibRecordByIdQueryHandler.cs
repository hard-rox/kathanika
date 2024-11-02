using Kathanika.Core.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Core.Application.Features.BibRecords.Queries;

internal sealed class GetBibRecordByIdQueryHandler(IBibRecordRepository bibRecordRepository)
    : IRequestHandler<GetBibRecordByIdQuery, Result<BibRecord>>
{
    public async Task<Result<BibRecord>> Handle(GetBibRecordByIdQuery request, CancellationToken cancellationToken)
    {
        BibRecord? bibRecord = await bibRecordRepository.GetByIdAsync(request.Id, cancellationToken);

        return bibRecord is null
            ? Result.Failure<BibRecord>(BibRecordAggregateErrors.NotFound(request.Id))
            : Result.Success(bibRecord);
    }
}
