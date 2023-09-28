namespace Kathanika.Application.Features.Authors.Commands;

public sealed record AddAuthorCommand(
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    DateOnly? DateOfDeath,
    string Nationality,
    string Biography
) : IRequest<Author>;
