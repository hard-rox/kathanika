using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibRecordAggregate;

/// <summary>
/// Represents MARC21 Field 300 - Physical Description (R)
/// Subfields:
/// $a - Extent (R)
/// $b - Other physical details (NR)
/// $c - Dimensions (R)
/// </summary>
public sealed record PhysicalDescription : ValueObject
{
    private List<string> _extents = [];
    private List<string> _dimensions = [];

    /// <summary>
    /// Extent (Subfield $a) - Repeatable
    /// The number and specific material designation of units of the item
    /// Examples: "xv, 123 p.", "2 videodiscs", "1 map"
    /// </summary>
    public IReadOnlyList<string> Extents
    {
        get => _extents;
        private set { _extents = value.ToList(); }
    }

    /// <summary>
    /// Dimensions (Subfield $c) - Repeatable
    /// Size of the item in centimeters, millimeters, inches, etc.
    /// Examples: "24 cm", "12 in.", "4 3/4 in."
    /// </summary>
    public IReadOnlyList<string> Dimensions
    {
        get => _dimensions;
        private set { _dimensions = value.ToList(); }
    }

    internal PhysicalDescription(
        string? extent,
        string? dimensions)
    {
        if (!string.IsNullOrWhiteSpace(extent))
            _extents = [extent];

        if (!string.IsNullOrWhiteSpace(dimensions))
            _dimensions = [dimensions];
    }

    public override IEnumerable<object?> GetAtomicValues()
    {
        yield return Extents;
        yield return Dimensions;
    }
}