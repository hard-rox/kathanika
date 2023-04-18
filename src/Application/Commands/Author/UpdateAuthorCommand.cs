namespace Kathanika.Application.Commands;

public sealed record UpdateAuthorCommand : IRequest<Author>
{
    public sealed record AuthorPatch(
    string? FirstName = null,
    string? LastName = null,
    DateTime? DateOfBirth = null,
    string? Nationality = null,
    string? Biography = null
    );

    public string Id { get; init; }
    public AuthorPatch Patch { get; init; }

    public UpdateAuthorCommand(string id, AuthorPatch patch)
    {
        Id = id;
        Patch = patch;
    }

}