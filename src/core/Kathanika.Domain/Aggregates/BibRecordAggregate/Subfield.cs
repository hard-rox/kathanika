using System.Text.RegularExpressions;
using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibRecordAggregate;

/// <summary>
/// MARC21 Subfield - Contains actual data with a single-character code.
/// Subfield codes are typically lowercase letters, digits, or special characters.
/// </summary>
public record Subfield : ValueObject
{
    private static readonly Regex SubfieldCodePattern = new(@"^[a-z0-9!$%]$", RegexOptions.Compiled, TimeSpan.FromMilliseconds(100));

    /// <summary>
    /// Single character subfield code (a-z, 0-9, !, $, %).
    /// </summary>
    public char Code { get; private init; }

    /// <summary>
    /// Variable-length data content of the subfield.
    /// </summary>
    public string Value { get; private init; }

    private Subfield()
    {
    }

    private Subfield(char code, string value)
    {
        Code = code;
        Value = value;
    }

    /// <summary>
    /// Creates a subfield with validation.
    /// Validates a code format and ensures the value is not empty.
    /// </summary>
    public static KnResult<Subfield> Create(char code, string value)
    {
        // Validate subfield code (a-z, 0-9, !, $, %)
        if (!SubfieldCodePattern.IsMatch(code.ToString()))
            return KnResult.Failure<Subfield>(new KnError("Subfield.InvalidCode",
                $"Subfield code must be a-z, 0-9, !, $, or %, got: '{code}'"));

        // Validate value is not empty
        if (string.IsNullOrEmpty(value))
            return KnResult.Failure<Subfield>(new KnError("Subfield.EmptyValue",
                $"Subfield {code} value cannot be empty"));

        Subfield subfield = new(code, value);
        return KnResult.Success(subfield);
    }

    public override IEnumerable<object?> GetAtomicValues()
    {
        yield return Code;
        yield return Value;
    }
}