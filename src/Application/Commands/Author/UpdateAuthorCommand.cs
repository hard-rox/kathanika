namespace Kathanika.Application.Commands;

public sealed record UpdateAuthorCommand(
    string Id,
    string? FirstName,
    string? LastName,
    DateTime? DateOfBirth,
    string? Nationality,
    string? Biography
) : IRequest<Author>;