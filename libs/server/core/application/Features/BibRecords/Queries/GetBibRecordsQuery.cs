using Kathanika.Core.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Core.Application.Features.BibRecords.Queries;

public sealed record GetBibRecordsQuery : IRequest<IQueryable<BibRecord>>;
