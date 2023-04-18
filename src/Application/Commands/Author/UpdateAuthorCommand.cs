namespace Kathanika.Application.Commands;

public sealed record UpdateAuthorCommand : IRequest<Author>
{
    public sealed record Patch(
    string? FirstName = null,
    string? LastName = null,
    DateTime? DateOfBirth = null,
    string? Nationality = null,
    string? Biography = null
);
    public string Id { get; init; }
    public Patch Data { get; init; }

    public UpdateAuthorCommand(string id, Patch data)
    {
        Id = id;
        Data = data;
    }

}