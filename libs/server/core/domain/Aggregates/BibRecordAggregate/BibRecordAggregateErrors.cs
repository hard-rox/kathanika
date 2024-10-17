using Kathanika.Core.Domain.Primitives;
namespace Kathanika.Core.Domain.Aggregates.BibRecordAggregate;

public static class BibRecordAggregateErrors
{
    public static readonly KnError LeaderInvalid = new(
        "BibRecord.LeaderInvalid",
        message: "Leader is required and must be exactly 24 characters long."
    );

    public static readonly KnError ControlNumberInvalid = new(
        "BibRecord.ControlNumberInvalid",
        message: "001 Control Number is required, unique, and must be less than 50 characters."
    );

    public static readonly KnError ControlNumberIdentifierInvalid = new(
        "BibRecord.ControlNumberIdentifierInvalid",
        message: "003 Control Number Identifier is required and must be less than 50 characters."
    );

    public static readonly KnError DateTimeOfLatestTransactionInvalid = new(
        "BibRecord.DateOfLatestTransactionInvalid",
        message: "005 Date and Time of Latest Transaction is required and must be a valid DateTime."
    );

    public static readonly KnError FixedLengthDataElementsInvalid = new(
        "BibRecord.FixedLengthDataElementsInvalid",
        message: "008 Fixed-Length Data Elements are required and must be exactly 40 characters long."
    );

    public static readonly KnError TranscribingAgencyInvalid = new(
        "040.c",
        message: "040.c TranscribingAgency is required."
    );
}
