namespace Kathanika.Application.Features.Members.Commands;

public sealed record SuspendMembershipCommand(string Id) : IRequest;
