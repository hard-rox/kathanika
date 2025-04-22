using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibRecordAggregate;

/// <summary>
/// Represents a bibliographic record with specific data elements.
/// </summary>
public sealed class BibRecord : AggregateRoot
{
    private static class ValidationRules
    {
        public const int LeaderLength = 24;
        public const int ControlNumberMaxLength = 50;
        public const int ControlNumberIdentifierMaxLength = 50;
        public const int FixedLengthDataElementsLength = 40;
    }

    private record struct ValidationResult(bool IsValid, KnError? Error);

    /// <summary>
    ///     000 (Record Leader) - Must be exactly 24 characters.
    /// </summary>
    public string Leader { get; private set; }

    /// <summary>
    ///     001 (Control Number) - Must be unique and less than 50 characters.
    /// </summary>
    public string ControlNumber { get; private set; }

    /// <summary>
    ///     003 (Control Number Identifier) - Must be less than 50 characters.
    /// </summary>
    public string ControlNumberIdentifier { get; private set; }

    /// <summary>
    ///     005 (Date and Time of Latest Transaction) - Must be a valid DateTime.
    /// </summary>
    public DateTime DateAndTimeOfLatestTransaction { get; private set; }

    /// <summary>
    ///     008 (Fixed-Length Data Elements) - Must be exactly 40 characters.
    /// </summary>
    public string FixedLengthDataElements { get; private set; }

    /// <summary>
    /// 020 (International Standard Book Number) - Optional
    /// Contains the International Standard Book Number (ISBN) assigned to the resource.
    /// </summary>
    public string? InternationalStandardBookNumber { get; private set; }

    /// <summary>
    /// 022 (International Standard Serial Number) - Optional
    /// Contains the International Standard Serial Number (ISSN) assigned to the resource.
    /// </summary>
    public string? InternationalStandardSerialNumber { get; private set; }

    /// <summary>
    ///     040 (Cataloging Source) - Must be a valid CatalogingSource.
    /// </summary>
    public CatalogingSource CatalogingSource { get; private set; }


    /// <summary>
    /// Represents the MARC21 Language Code field (041), containing information
    /// about the language(s) of the resource, such as the text or sound track
    /// and optionally the original language.
    /// </summary>
    public LanguageCode LanguageCode { get; private set; }

    /// <summary>
    /// Represents the MARC21 Title Statement (Field 245).
    /// Contains the title and optionally the remainder of the title
    /// and statement of responsibility.
    /// </summary>
    public TitleStatement TitleStatement { get; private set; }


    public PublicationDistribution PublicationDistribution { get; private set; }

    private BibRecord(
        string leader,
        string controlNumber,
        string controlNumberIdentifier,
        DateTime dateTimeOfLatestTransaction,
        string fixedLengthDataElements,
        string? internationalStandardBookNumber,
        string? internationalStandardSerialNumber,
        CatalogingSource catalogingSource)
    {
        Leader = leader;
        ControlNumber = controlNumber;
        ControlNumberIdentifier = controlNumberIdentifier;
        DateAndTimeOfLatestTransaction = dateTimeOfLatestTransaction;
        FixedLengthDataElements = fixedLengthDataElements;
        CatalogingSource = catalogingSource;
        InternationalStandardBookNumber = internationalStandardBookNumber;
        InternationalStandardSerialNumber = internationalStandardSerialNumber;
    }

    private static ValidationResult ValidateStringField(string input, int? maxLength, KnError errorType)
    {
        var isValid = !string.IsNullOrWhiteSpace(input) &&
                      (!maxLength.HasValue || input.Length <= maxLength.Value);
        return new ValidationResult(isValid, isValid ? null : errorType);
    }

    private static ValidationResult ValidateDateTime(DateTime dateTime, KnError errorType)
    {
        var isValid = dateTime != default;
        return new ValidationResult(isValid, isValid ? null : errorType);
    }

    public static KnResult<BibRecord> Create(
        string leader,
        string controlNumber,
        string controlNumberIdentifier,
        DateTime dateTimeOfLatestTransaction,
        string fixedLengthDataElements,
        string? internationalStandardBookNumber,
        string? internationalStandardSerialNumber,
        CatalogingSource catalogingSource)
    {
        ValidationResult[] validations =
        [
            ValidateStringField(leader, ValidationRules.LeaderLength, BibRecordAggregateErrors.LeaderInvalid),
            ValidateStringField(controlNumber, ValidationRules.ControlNumberMaxLength,
                BibRecordAggregateErrors.ControlNumberInvalid),
            ValidateStringField(controlNumberIdentifier, ValidationRules.ControlNumberIdentifierMaxLength,
                BibRecordAggregateErrors.ControlNumberIdentifierInvalid),
            ValidateDateTime(dateTimeOfLatestTransaction, BibRecordAggregateErrors.DateTimeOfLatestTransactionInvalid),
            ValidateStringField(fixedLengthDataElements, ValidationRules.FixedLengthDataElementsLength,
                BibRecordAggregateErrors.FixedLengthDataElementsInvalid)
        ];

        List<KnError> errors = validations
            .Where(v => !v.IsValid)
            .Select(v => v.Error!)
            .ToList();

        if (errors.Count != 0)
            return KnResult.Failure<BibRecord>(errors);

        BibRecord record = new(
            leader,
            controlNumber,
            controlNumberIdentifier,
            dateTimeOfLatestTransaction,
            fixedLengthDataElements,
            internationalStandardBookNumber,
            internationalStandardSerialNumber,
            catalogingSource);

        return KnResult.Success(record);
    }
}