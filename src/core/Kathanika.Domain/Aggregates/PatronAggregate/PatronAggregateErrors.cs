using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.PatronAggregate;

public static class PatronAggregateErrors
{
    public static readonly KnError CancelledPatronship = new(
        "Patron.CancelledPatronship",
        "Patronship is cancelled"
    );

    public static readonly KnError ActivePatronship = new(
        "Patron.CancelledPatronship",
        "Patronship is active"
    );

    public static readonly KnError FutureDateOfBirth = new(
        "Patron.FutureDateOfBirth",
        "Date of birth cannot be future date"
    );

    public static KnError HasIssuedPublication(int issuedPublicationLength)
    {
        return new KnError(
            "Patron.HasIssuedPublication",
            $"Patron has issued {issuedPublicationLength} publications."
        );
    }

    public static KnError NotFound(string id)
    {
        return new KnError(
            "Patron.NotFound",
            $"No Patron found with this Id: \"{id}\""
        );
    }
}