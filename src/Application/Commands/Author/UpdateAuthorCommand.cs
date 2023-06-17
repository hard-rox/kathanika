namespace Kathanika.Application.Commands;

public sealed record UpdateAuthorCommand : IRequest<Author>
{
    public string Id { get; init; }
    public AuthorPatch Patch { get; init; }
    
    public sealed record AuthorPatch(
    string? FirstName = null,
    string? LastName = null,
    DateOnly? DateOfBirth = null,
    string? Nationality = null,
    string? Biography = null
    );

    public UpdateAuthorCommand(string id, AuthorPatch patch)
    {
        Id = id;
        Patch = patch;
    }

}