namespace Kathanika.Core.Application.Features.Members.Commands;

public sealed record RenewMembershipCommand(string Id) : IRequest<Result<Member>>;
