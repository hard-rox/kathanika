namespace Kathanika.Application.Commands;

public sealed record AddPublicationCommand(
    string Title,
    string Isbn,
    PublicationType PublicationType,
    IEnumerable<string> AuthorIds,
    string Publisher,
    DateOnly PublishedDate,
    decimal BuyingPrice,
    int CopiesPurchased,
    string CallNumber) : IRequest<Publication>;
