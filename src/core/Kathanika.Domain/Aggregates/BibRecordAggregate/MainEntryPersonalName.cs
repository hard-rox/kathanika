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

    internal MainEntryPersonalName(string personalName)
    {
        PersonalName = personalName;
    }

    public override IEnumerable<object?> GetAtomicValues()
    {
        yield return PersonalName;
    }
}