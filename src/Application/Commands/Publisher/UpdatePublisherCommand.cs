namespace Kathanika.Application.Commands;

public sealed record UpdatePublisherCommand : IRequest<Publisher>
{
    public string Id { get; init; }
    public PublisherPatch Patch { get; init; }

    public sealed record PublisherPatch(
        string PublisherName,
        string Description,
        string ContactInformation
    );

    public UpdatePublisherCommand(string id, PublisherPatch patch)
    {
        this.Id = id;
        this.Patch = patch;
    }
}
