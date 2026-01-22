using Kathanika.Domain.Aggregates.BibItemAggregate;
namespace Kathanika.Application.Features.BibItems.Queries;

public sealed record GetBibItemsQuery(string BibRecordId) : IQuery<IQueryable<BibItem>>;