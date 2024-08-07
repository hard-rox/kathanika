namespace Kathanika.Core.Application.Features.Members.Commands;

public sealed record SuspendMembershipCommand(string Id) : IRequest<Result<Member>>;
