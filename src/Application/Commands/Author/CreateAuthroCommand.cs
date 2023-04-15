namespace Kathanika.Application.Commands;

public sealed record CreateAuthorCommand(
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    DateTime? DateOfDeath,
    string Nationality,
    string Biography
) : IRequest<Author>;