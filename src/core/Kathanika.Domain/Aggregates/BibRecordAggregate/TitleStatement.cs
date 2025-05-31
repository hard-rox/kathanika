using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibRecordAggregate;

/// <summary>
/// Represents MARC21 Field 245 - Title Statement (NR)
/// </summary>
public sealed record TitleStatement : ValueObject
{
    /// <summary>
    /// Title proper (Subfield $a) - Required, Non-repeatable
    /// The main title of the work, excluding alternative titles
    /// </summary>
    public string Title { get; private set; }

    /// <summary>
    /// Remainder of title (Subfield $b) - Optional, Non-repeatable
    /// Subtitle and other title information that appears after the main title
    /// </summary>
    public string? RemainderOfTitle { get; private set; }

    /// <summary>
    /// Statement of responsibility (Subfield $c) - Optional, Non-repeatable
    /// Contains names and/or organizations responsible for the creation of the work
    /// </summary>
    public string? StatementOfResponsibility { get; private set; }

    private TitleStatement(
        string title,
        string? remainderOfTitle = null,
        string? statementOfResponsibility = null)
    {
        Title = title;
        RemainderOfTitle = remainderOfTitle;
        StatementOfResponsibility = statementOfResponsibility;
    }

    internal static KnResult<TitleStatement> Create(
        string title,
        string? remainderOfTitle = null,
        string? statementOfResponsibility = null)
    {
        return string.IsNullOrWhiteSpace(title)
            ? KnResult.Failure<TitleStatement>(BibRecordAggregateErrors.TitleRequired)
            : KnResult.Success(new TitleStatement(title, remainderOfTitle, statementOfResponsibility));
    }

    public override IEnumerable<object?> GetAtomicValues()
    {
        yield return Title;
        yield return RemainderOfTitle;
        yield return StatementOfResponsibility;
    }
    
    private TitleStatement() { }
}