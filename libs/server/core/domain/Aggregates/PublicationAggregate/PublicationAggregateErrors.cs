using Kathanika.Core.Domain.Primitives;

namespace Kathanika.Core.Domain.Aggregates.PublicationAggregate;

public static class PublicationAggregateErrors
{
    public static readonly KnError UnitPriceNull = new(
        "Publication.UnitPriceNull",
        message: "Unit price is required"
    );

    public static readonly KnError VendorNull = new(
        "Publication.VendorNull",
        message: "Vendor is required"
    );

    public static readonly KnError PatronNull = new(
        "Publication.PatronNull",
        message: "Patron is required"
    );

    public static KnError NotFound(string id) => new(
        "Publication.NotFound",
        message: $"No Publication found with this Id: \"{id}\""
    );

    public static KnError AuthorNotFound(string id) => new(
        "Publication.AuthorNotFound",
        message: $"No Publication Author found found with this Id: \"{id}\""
    );
}
