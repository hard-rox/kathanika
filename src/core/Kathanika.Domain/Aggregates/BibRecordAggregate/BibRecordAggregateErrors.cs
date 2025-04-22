using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibRecordAggregate;

public static class BibRecordAggregateErrors
{
    public static readonly KnError LeaderInvalid = new(
        "BibRecord.LeaderInvalid",
        "Leader is required and must be exactly 24 characters long."
    );

    public static readonly KnError ControlNumberInvalid = new(
        "BibRecord.ControlNumberInvalid",
        "001 Control Number is required, unique, and must be less than 50 characters."
    );

    public static readonly KnError ControlNumberIdentifierInvalid = new(
        "BibRecord.ControlNumberIdentifierInvalid",
        "003 Control Number Identifier is required and must be less than 50 characters."
    );

    public static readonly KnError DateTimeOfLatestTransactionInvalid = new(
        "BibRecord.DateOfLatestTransactionInvalid",
        "005 Date and Time of Latest Transaction is required and must be a valid DateTime."
    );

    public static readonly KnError FixedLengthDataElementsInvalid = new(
        "BibRecord.FixedLengthDataElementsInvalid",
        "008 Fixed-Length Data Elements are required and must be exactly 40 characters long."
    );

    public static readonly KnError TranscribingAgencyInvalid = new(
        "BibRecord.TranscribingAgencyInvalid",
        "040.c Transcribing Agency is required."
    );

    public static readonly KnError IsbnInvalid = new(
        "BibRecord.IsbnInvalid",
        "020 ISBN must be a valid ISBN format."
    );

    public static readonly KnError IssnInvalid = new(
        "BibRecord.ISSNInvalid",
        "022 ISSN must be a valid ISSN format."
    );

    public static readonly KnError LanguageCodeInvalid = new(
        "BibRecord.LanguageCodeInvalid",
        "041 Language Code is required and must be valid."
    );

    public static readonly KnError TitleRequired = new(
        "BibRecord.TitleRequired",
        "245 Title is required."
    );

    public static KnError NotFound(string id)
    {
        return new KnError("BibRecord.NotFound",
            $"No Bib Record found with this Id: \"{id}\"");
    }
}