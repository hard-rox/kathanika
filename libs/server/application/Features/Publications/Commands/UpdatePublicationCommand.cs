namespace Kathanika.Application.Features.Publications.Commands;

public sealed record UpdatePublicationCommand(string Id, PublicationPatch Patch) : IRequest<Publication>;

public sealed record PublicationPatch(
        string Title,
        string Isbn,
        PublicationType PublicationType,
        string Publisher,
        DateOnly? PublishedDate,
        string? Edition,
        string CallNumber,
        string? Description,
        IEnumerable<string>? AuthorIds = null
    );
