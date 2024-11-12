using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Application.Features.BibRecords.Queries;

internal sealed class GetBibRecordsQueryHandler(IBibRecordRepository bibRecordRepository) : IRequestHandler<GetBibRecordsQuery, IQueryable<BibRecord>>
{
    public async Task<IQueryable<BibRecord>> Handle(GetBibRecordsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<BibRecord> bibRecordsQuery = await Task.Run(bibRecordRepository.AsQueryable);
        return bibRecordsQuery;
    }
}