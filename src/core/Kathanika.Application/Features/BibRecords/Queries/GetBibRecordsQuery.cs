using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Application.Features.BibRecords.Queries;

public sealed record GetBibRecordsQuery : IRequest<IQueryable<BibRecord>>;