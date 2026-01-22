using Kathanika.Domain.Aggregates.BibItemAggregate;
namespace Kathanika.Application.Features.BibItems.Queries;

public sealed record GetBibItemByIdQuery(string Id) : IQuery<KnResult<BibItem>>;