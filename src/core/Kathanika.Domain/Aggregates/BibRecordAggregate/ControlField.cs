using System.Text.RegularExpressions;
using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibRecordAggregate;

/// <summary>
/// MARC21 Control Field (001-009) - Contains control information without indicators or subfields.
/// Examples: 001 (Control Number), 003 (Control Number Identifier), 005 (Date/Time), 008 (Fixed Data).
/// </summary>
public record ControlField : ValueObject
{
    private static readonly Regex ControlFieldTagPattern = new(@"^00[1-9]$", RegexOptions.Compiled);

    /// <summary>
    /// Three-digit tag identifying the field (001-009).
    /// </summary>
    public string Tag { get; private init; }

    /// <summary>
    /// Variable-length data content of the control field.
    /// </summary>
    public string Data { get; private init; }

    private ControlField()
    {
    }

    private ControlField(string tag, string data)
    {
        Tag = tag;
        Data = data;
    }

    internal static KnResult<ControlField> Create(string tag, string data)
    {
        if (string.IsNullOrWhiteSpace(tag))
            return KnResult.Failure<ControlField>(BibRecordAggregateErrors.ControlField.EmptyTag);

        if (!ControlFieldTagPattern.IsMatch(tag))
            return KnResult.Failure<ControlField>(BibRecordAggregateErrors.ControlField.InvalidTag(tag));

        return string.IsNullOrWhiteSpace(data)
            ? KnResult.Failure<ControlField>(BibRecordAggregateErrors.ControlField.EmptyData(tag))
            : KnResult.Success(new ControlField(tag, data));
    }

    public override IEnumerable<object?> GetAtomicValues()
    {
        yield return Tag;
        yield return Data;
    }
}