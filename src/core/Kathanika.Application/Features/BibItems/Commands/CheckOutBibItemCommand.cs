using Kathanika.Domain.Aggregates.BibItemAggregate;

namespace Kathanika.Application.Features.BibItems.Commands;

public sealed record CheckOutBibItemCommand(string Id) : IRequest<KnResult<BibItem>>;
