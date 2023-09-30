namespace Kathanika.Application.Features.Publications.Commands;

public sealed record UpdatePublicationCommand : IRequest<Publication>
{
    public string Id { get; init; }
    public PublicationPatch Patch { get; init; }

    public sealed record PublicationPatch(
        string Title,
        string Isbn,
        PublicationType PublicationType,
        string Publisher,
        DateOnly? PublishedDate,
        string? Edition,
        decimal? BuyingPrice,
        int? CopiesAvailable,
        string CallNumber,
        IEnumerable<string>? AuthorIds = null
    );

    public UpdatePublicationCommand(string id, PublicationPatch patch)
    {
        Id = id;
        Patch = patch;
    }
}
