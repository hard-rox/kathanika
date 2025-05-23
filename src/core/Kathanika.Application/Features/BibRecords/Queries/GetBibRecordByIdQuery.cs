using Kathanika.Domain.Aggregates.BibRecordAggregate;

namespace Kathanika.Application.Features.BibRecords.Queries;

public sealed record GetBibRecordByIdQuery(string Id) : IRequest<KnResult<BibRecord>>;