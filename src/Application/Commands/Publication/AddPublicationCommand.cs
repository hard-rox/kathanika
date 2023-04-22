namespace Kathanika.Application.Commands;

public sealed record AddPublicationCommand(
    string Title,
    string Isbn,
    PublicationType PublicationType,
    IEnumerable<string> AuthorIds,
    string Publisher,
    DateTime PublishedDate,
    decimal BuyingPrice,
    int CopiesPurchased) : IRequest<Publication>;
