using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibRecordAggregate;

/// <summary>
/// Represents MARC21 Field 100 - Main Entry - Personal Name (NR)
/// Field contains the name of the person considered to be primarily responsible for the work.
/// </summary>
public sealed record MainEntryPersonalName : ValueObject
{
    /// <summary>
    /// Personal name (Subfield $a) - Required, Non-repeatable
    /// Contains the personal name in direct or indirect order.
    /// </summary>
    public string PersonalName { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MainEntryPersonalName"/> record with the specified personal name.
    /// </summary>
    /// <param name="personalName">The personal name associated with the main entry (MARC21 Field 100).</param>
    internal MainEntryPersonalName(string personalName)
    {
        PersonalName = personalName;
    }

    /// <summary>
    /// Returns the atomic values used for value-based equality, yielding the personal name.
    /// </summary>
    /// <returns>An enumerable containing the personal name.</returns>
    public override IEnumerable<object?> GetAtomicValues()
    {
        yield return PersonalName;
    }
}