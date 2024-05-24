namespace Kathanika.Application.Features.Publications.Commands;

public sealed record UpdatePublicationCommand(string Id, PublicationPatch Patch) : IRequest<Publication>;

public sealed record PublicationPatch(
        string Title,
        string? Isbn,
        PublicationType PublicationType,
        string PublisherId,
        DateOnly? PublishedDate,
        string? Edition,
        string CallNumber,
        string? Description,
        string? Language,
        IEnumerable<string>? AuthorIds = null
    );
