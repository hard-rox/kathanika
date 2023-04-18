namespace Kathanika.Application.Commands;

public sealed record AddAuthorCommand(
    string FirstName,
    string LastName,
    DateTime DateOfBirth,
    DateTime? DateOfDeath,
    string Nationality,
    string Biography
) : IRequest<Author>;