namespace Kathanika.Application.Features.Members.Commands;

public sealed record AddMemberCommand(
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    string Address,
    string ContactNumber,
    string Email
) : IRequest<Member>;
