using Kathanika.Domain.Aggregates.BibItemAggregate;
namespace Kathanika.Application.Features.BibItems.Commands;

public sealed record WithdrawBibItemCommand(string Id, string? Reason = null) : ICommand<KnResult<BibItem>>;