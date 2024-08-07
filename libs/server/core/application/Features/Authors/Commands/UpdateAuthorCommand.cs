namespace Kathanika.Core.Application.Features.Authors.Commands;

public sealed record UpdateAuthorCommand(string Id, AuthorPatch Patch) : IRequest<Result<Author>>;

public sealed record AuthorPatch(
        string? FirstName = null,
        string? LastName = null,
        DateOnly? DateOfBirth = null,
        string? Nationality = null,
        string? Biography = null,
        bool MarkedAsDeceased = false,
        DateOnly? DateOfDeath = null,
        string? DpFileId = null
    );
