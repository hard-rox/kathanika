namespace Kathanika.Core.Application.Features.Members.Commands;

public sealed record CreateMemberCommand(
    string FirstName,
    string LastName,
    string PhotoFileId,
    DateOnly DateOfBirth,
    string Address,
    string ContactNumber,
    string Email
) : IRequest<Member>;
