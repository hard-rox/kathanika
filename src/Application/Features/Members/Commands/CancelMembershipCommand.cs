namespace Kathanika.Application.Features.Members.Commands;

public sealed record CancelMembershipCommand(string Id) : IRequest;
