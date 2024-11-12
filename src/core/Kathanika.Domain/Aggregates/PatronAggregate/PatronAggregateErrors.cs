using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.PatronAggregate;

public static class PatronAggregateErrors
{
    public static KnError HasIssuedPublication(int issuedPublicationLength) => new(
        "Patron.HasIssuedPublication",
        message: $"Patron has issued {issuedPublicationLength} publications."
    );

    public static KnError NotFound(string id) => new(
        "Patron.NotFound",
        message: $"No Patron found with this Id: \"{id}\""
    );

    public static readonly KnError CancelledPatronship = new(
        "Patron.CancelledPatronship",
        message: "Patronship is cancelled"
    );

    public static readonly KnError ActivePatronship = new(
        "Patron.CancelledPatronship",
        message: "Patronship is active"
    );

    public static readonly KnError FutureDateOfBirth = new(
        "Patron.FutureDateOfBirth",
        message: "Date of birth cannot be future date"
    );
}