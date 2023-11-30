namespace Kathanika.Application.Features.Publications.Commands;

public sealed record AddPublicationCommand(
    string Title,
    string Isbn,
    PublicationType PublicationType,
    IEnumerable<string> AuthorIds,
    string Publisher,
    DateOnly PublishedDate,
    string Edition,
    decimal BuyingPrice,
    int CopiesPurchased,
    string CallNumber) : IRequest<Publication>;
