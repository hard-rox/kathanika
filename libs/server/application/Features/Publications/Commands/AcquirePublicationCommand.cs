namespace Kathanika.Application.Features.Publications.Commands;

public sealed record AcquirePublicationCommand(
    string Title,
    string? Isbn,
    PublicationType PublicationType,
    IEnumerable<string> AuthorIds,
    string Publisher,
    DateOnly PublishedDate,
    string Edition,
    string CallNumber,
    string? Description,
    string Language,
    AcquisitionMethod AcquisitionMethod,
    int Quantity,
    decimal? UnitPrice,
    string? Vendor,
    string? Patron) : IRequest<Publication>;
