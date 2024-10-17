using Kathanika.Core.Domain.Primitives;

namespace Kathanika.Core.Domain.Aggregates.BibliographicRecordAggregate;

/// <summary>
/// Represents a bibliographic record with specific data elements.
/// </summary>
/// <remarks>
/// This class enforces validation rules for each data element and provides a method to create a new bibliographic record instance.
/// </remarks>
public sealed class BibliographicRecord : AggregateRoot
{
    /// <summary>
    /// 000 (Record Leader) - Must be exactly 24 characters.
    /// </summary>
    public string Leader { get; private set; }

    /// <summary>
    /// 001 (Control Number) - Must be unique and less than 50 characters.
    /// </summary>
    public string ControlNumber { get; private set; }

    /// <summary>
    /// 003 (Control Number Identifier) - Must be less than 50 characters.
    /// </summary>
    public string ControlNumberIdentifier { get; private set; }

    /// <summary>
    /// 005 (Date and Time of Latest Transaction) - Must be a valid DateTime.
    /// </summary>
    public DateTime DateAndTimeOfLatestTransaction { get; private set; }

    /// <summary>
    /// 008 (Fixed-Length Data Elements) - Must be exactly 40 characters.
    /// </summary>
    public string FixedLengthDataElements { get; private set; }

    /// <summary>
    /// 040 (Cataloging Source) - Must be a valid CatalogingSource.
    /// </summary>
    public CatalogingSource CatalogingSource { get; private set; }

    private BibliographicRecord(string leader,
        string controlNumber,
        string controlNumberIdentifier,
        DateTime dateTimeOfLatestTransaction,
        string fixedLengthDataElements,
        CatalogingSource catalogingSource)
    {
        Leader = leader;
        ControlNumber = controlNumber;
        ControlNumberIdentifier = controlNumberIdentifier;
        DateAndTimeOfLatestTransaction = dateTimeOfLatestTransaction;
        FixedLengthDataElements = fixedLengthDataElements;
        CatalogingSource = catalogingSource;
    }

    private static void ValidateInput(string input, int? maxLength, KnError errorType, List<KnError> errors)
    {
        if (string.IsNullOrWhiteSpace(input) || (maxLength.HasValue && input.Length > maxLength.Value))
            errors.Add(errorType);
    }

    private static void ValidateDateTime(DateTime dateTime, KnError errorType, List<KnError> errors)
    {
        if (dateTime == default)
            errors.Add(errorType);
    }

    public static Result<BibliographicRecord> Create(string leader,
        string controlNumber,
        string controlNumberIdentifier,
        DateTime dateTimeOfLatestTransaction,
        string fixedLengthDataElements,
        CatalogingSource catalogingSource)
    {
        List<KnError> errors = [];

        ValidateInput(leader, 24, BibliographicRecordAggregateErrors.LeaderInvalid, errors);
        ValidateInput(controlNumber, 50, BibliographicRecordAggregateErrors.ControlNumberInvalid, errors);
        ValidateInput(controlNumberIdentifier, 50, BibliographicRecordAggregateErrors.ControlNumberIdentifierInvalid, errors);
        ValidateDateTime(dateTimeOfLatestTransaction, BibliographicRecordAggregateErrors.DateTimeOfLatestTransactionInvalid, errors);
        ValidateInput(fixedLengthDataElements, 40, BibliographicRecordAggregateErrors.FixedLengthDataElementsInvalid, errors);

        if (errors.Count > 0)
            return Result.Failure<BibliographicRecord>(errors);

        BibliographicRecord record = new(leader,
            controlNumber,
            controlNumberIdentifier,
            dateTimeOfLatestTransaction,
            fixedLengthDataElements,
            catalogingSource);

        return Result.Success(record);
    }
}
