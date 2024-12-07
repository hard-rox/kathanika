using Kathanika.Domain.Aggregates.BibRecordAggregate;
using Kathanika.Domain.Primitives;

namespace Kathanika.Application.Features.BibRecords.Queries;

internal sealed class GetBibRecordByIdQueryHandler(IBibRecordRepository bibRecordRepository)
    : IRequestHandler<GetBibRecordByIdQuery, KnResult<BibRecord>>
{
    public async Task<KnResult<BibRecord>> Handle(GetBibRecordByIdQuery request, CancellationToken cancellationToken)
    {
        BibRecord? bibRecord = await bibRecordRepository.GetByIdAsync(request.Id, cancellationToken);

        return bibRecord is null
            ? KnResult.Failure<BibRecord>(BibRecordAggregateErrors.NotFound(request.Id))
            : KnResult.Success(bibRecord);
    }
}