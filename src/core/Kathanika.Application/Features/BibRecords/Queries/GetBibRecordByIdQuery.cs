using Kathanika.Domain.Aggregates.BibRecordAggregate;
using Kathanika.Domain.Primitives;

namespace Kathanika.Application.Features.BibRecords.Queries;

public sealed record GetBibRecordByIdQuery(string Id) : IRequest<Result<BibRecord>>;