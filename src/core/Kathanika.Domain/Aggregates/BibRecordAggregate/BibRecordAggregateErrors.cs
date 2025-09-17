using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibRecordAggregate;

public static class BibRecordAggregateErrors
{
    public static class ControlField
    {
        public static readonly KnError EmptyTag = new("ControlField.EmptyTag", "Control field tag cannot be null or empty");

        public static KnError InvalidTag(string tag) =>
            new("ControlField.InvalidTag", $"Control field tag must be 001-009, got: {tag}");

        public static KnError EmptyData(string tag) =>
            new("ControlField.EmptyData", $"Control field {tag} data cannot be empty");
    }

    public static readonly KnError LeaderInvalid = new(
        "BibRecord.LeaderInvalid",
        "Leader is required and must be exactly 24 characters long."
    );

    public static readonly KnError ControlNumberInvalid = new(
        "BibRecord.ControlNumberInvalid",
        "001 Control Number is required, unique, and must be less than 50 characters."
    );

    public static readonly KnError DateTimeOfLatestTransactionInvalid = new(
        "BibRecord.DateOfLatestTransactionInvalid",
        "005 Date and Time of Latest Transaction is required and must be a valid DateTime."
    );

    public static KnError NotFound(string id)
    {
        return new KnError("BibRecord.NotFound",
            $"No Bib Record found with this Id: \"{id}\"");
    }
}