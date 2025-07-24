using Kathanika.Domain.Aggregates.BibItemAggregate;

namespace Kathanika.Application.Features.BibItems.Commands;

public sealed record CheckInBibItemCommand(string Id) : IRequest<KnResult<BibItem>>;
