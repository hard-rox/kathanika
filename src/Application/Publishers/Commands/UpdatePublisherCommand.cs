namespace Kathanika.Application.Publishers.Commands;

public sealed record UpdatePublisherCommand : IRequest<Publisher>
{
    public string Id { get; init; }
    public PublisherPatch Patch { get; init; }

    public sealed record PublisherPatch(
        string Name,
        string? Description,
        string? ContactInformation
    );

    public UpdatePublisherCommand(string id, PublisherPatch patch)
    {
        this.Id = id;
        this.Patch = patch;
    }
}
