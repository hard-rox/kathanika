using Kathanika.Core.Domain.Primitives;

namespace Kathanika.Core.Domain.Aggregates.AuthorAggregate;

public static class AuthorAggregateErrors
{
    public static readonly KnError FutureDateOfBirth = new(
        "Author.FutureDateOfBirth",
        message: "Date of birth cannot be future date"
    );

    public static readonly KnError FutureDateOfDeath = new(
        "Author.FutureDateOfDeath",
        message: "Date of death cannot be future date"
    );

    public static readonly KnError DateOfBirthFollowingDateOfDeath = new(
        "Author.DateOfBirthFollowingDateOfDeath",
        message: "DateOfDeath must be after DateOfBirth"
    );

    public static KnError NotFound(string id) => new(
        "Author.NotFound",
        message: $"No Author found with this Id: \"{id}\""
    );

    public static readonly KnError HasPublication = new(
        "Author.HasPublication",
        message: "This author has one or more publications in this library."
    );
}
