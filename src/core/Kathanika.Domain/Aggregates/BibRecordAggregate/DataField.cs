using System.Text.RegularExpressions;
using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibRecordAggregate;

/// <summary>
/// MARC21 Data Field (010-999) - Contains bibliographic data with indicators and subfields.
/// Examples: 020 (ISBN), 100 (Main Entry-Personal Name), 245 (Title Statement).
/// </summary>
public record DataField : ValueObject
{
    private static readonly Regex DataFieldTagPattern = new(@"^(0[1-9]\d|[1-9]\d\d)$", RegexOptions.Compiled, TimeSpan.FromMilliseconds(100));
    private static readonly Regex IndicatorPattern = new(@"^[\d ]$", RegexOptions.Compiled, TimeSpan.FromMilliseconds(100));

    /// <summary>
    /// Three-digit tag identifying the field (010-999).
    /// </summary>
    public string Tag { get; private init; }

    /// <summary>
    /// First indicator - Single character (digit 0-9 or space) providing field interpretation.
    /// </summary>
    public char Indicator1 { get; private init; } = ' ';

    /// <summary>
    /// Second indicator - Single character (digit 0-9 or space) providing field interpretation.
    /// </summary>
    public char Indicator2 { get; private init; } = ' ';

    /// <summary>
    /// Subfields containing the actual data with subfield codes.
    /// </summary>
    public IReadOnlyList<Subfield> Subfields { get; init; } = [];

    private DataField()
    {
    }

    private DataField(string tag, char indicator1, char indicator2, IEnumerable<Subfield> subfields)
    {
        Tag = tag;
        Indicator1 = indicator1;
        Indicator2 = indicator2;
        Subfields = subfields.ToList();
    }

    /// <summary>
    /// Creates a data field with validation.
    /// Validates a tag format, indicators, and subfields according to MARC21 standards.
    /// </summary>
    public static KnResult<DataField> Create(
        string tag,
        char indicator1,
        char indicator2,
        IEnumerable<Subfield> subfields)
    {
        // Validate tag format (010-999)
        if (!DataFieldTagPattern.IsMatch(tag))
            return KnResult.Failure<DataField>(new KnError("DataField.InvalidTag",
                $"Data field tag must be 010-999, got: {tag}"));

        // Validate indicators (digit or space)
        if (!IndicatorPattern.IsMatch(indicator1.ToString()))
            return KnResult.Failure<DataField>(new KnError("DataField.InvalidIndicator1",
                $"Indicator1 must be digit 0-9 or space, got: '{indicator1}'"));

        if (!IndicatorPattern.IsMatch(indicator2.ToString()))
            return KnResult.Failure<DataField>(new KnError("DataField.InvalidIndicator2",
                $"Indicator2 must be digit 0-9 or space, got: '{indicator2}'"));

        // Validate subfields
        if (subfields is null || !subfields.Any())
            return KnResult.Failure<DataField>(new KnError("DataField.NoSubfields",
                $"Data field {tag} must contain at least one subfield"));

        List<Subfield> subfieldsList = subfields.ToList();
        DataField dataField = new(
            tag,
            indicator1,
            indicator2,
            subfieldsList);
        return KnResult.Success(dataField);
    }

    public override IEnumerable<object?> GetAtomicValues()
    {
        yield return Tag;
        yield return Indicator1;
        yield return Indicator2;
        foreach (Subfield subfield in Subfields)
        {
            yield return subfield;
        }
    }
}