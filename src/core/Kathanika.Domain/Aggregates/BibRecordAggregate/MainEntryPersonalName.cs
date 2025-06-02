using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibRecordAggregate;

/// <summary>
/// Represents MARC21 Field 100 - Main Entry - Personal Name (NR)
/// Field contains the name of the person considered to be primarily responsible for the work.
/// </summary>
public sealed record MainEntryPersonalName : ValueObject
{
    private List<string> _relatorTerms = [];

    /// <summary>
    /// Personal name (Subfield $a) - Required, Non-repeatable
    /// Contains the personal name in direct or indirect order.
    /// </summary>
    public string PersonalName { get; private set; }

    /// <summary>
    /// Relator term (Subfield $e) - Optional, Non-repeatable
    /// Specifies the relationship between the person named in the field and the work.
    /// </summary>
    public IReadOnlyList<string> RelatorTerms
    {
        get => _relatorTerms;
        private set => _relatorTerms = [.. value];
    }

    private MainEntryPersonalName()
    {
    }

    internal MainEntryPersonalName(string personalName, string[] relatorTerms)
    {
        PersonalName = personalName;
        RelatorTerms = relatorTerms;
    }

    public override IEnumerable<object?> GetAtomicValues()
    {
        yield return PersonalName;
    }
}