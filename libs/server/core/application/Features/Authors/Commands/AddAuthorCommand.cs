namespace Kathanika.Core.Application.Features.Authors.Commands;

public sealed record AddAuthorCommand(
    string FirstName,
    string LastName,
    DateOnly DateOfBirth,
    string Nationality,
    string Biography,
    DateOnly? DateOfDeath,
    string? DpFileId = null,
    bool MarkedAsDeceased = false
) : IRequest<Author>;
