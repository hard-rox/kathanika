namespace Kathanika.Core.Application.Features.Members.Commands;

public sealed record CancelMembershipCommand(string Id) : IRequest<Member>;
