namespace Kathanika.Application.Features.Publications.Commands;

public sealed record PurchasePublicationCommand(
    string? PublicationId,
    string? Title,
    string? Isbn,
    PublicationType? PublicationType,
    IEnumerable<string>? AuthorIds,
    string? Publisher,
    DateOnly? PublishedDate,
    string? Edition,
    string? CallNumber,
    string? Description,
    string? Language,
    decimal UnitPrice,
    int Quantity,
    string Vendor
) : IRequest<Publication>;
