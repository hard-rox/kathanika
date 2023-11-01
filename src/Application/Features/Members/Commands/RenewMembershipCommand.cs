namespace Kathanika.Application.Features.Members.Commands;

public sealed record RenewMembershipCommand(string Id) : IRequest;
