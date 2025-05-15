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

    /// <summary>
    /// Initializes a new instance of the <see cref="TitleStatement"/> record with the specified title, optional remainder of title, and optional statement of responsibility.
    /// </summary>
    /// <param name="title">The main title of the work. Must not be null or whitespace.</param>
    /// <param name="remainderOfTitle">Optional subtitle or additional title information.</param>
    /// <param name="statementOfResponsibility">Optional statement indicating responsibility for the work.</param>
    private TitleStatement(
        string title,
        string? remainderOfTitle = null,
        string? statementOfResponsibility = null)
    {
        Title = title;
        RemainderOfTitle = remainderOfTitle;
        StatementOfResponsibility = statementOfResponsibility;
    }

    /// <summary>
    /// Creates a new <see cref="TitleStatement"/> instance if the title is provided; otherwise, returns a failure result.
    /// </summary>
    /// <param name="title">The main title of the work. Must not be null, empty, or whitespace.</param>
    /// <param name="remainderOfTitle">Optional subtitle or additional title information.</param>
    /// <param name="statementOfResponsibility">Optional statement of responsibility for the work.</param>
    /// <returns>
    /// A successful result containing the <see cref="TitleStatement"/> if the title is valid; otherwise, a failure result with an error indicating the title is required.
    /// </returns>
    internal static KnResult<TitleStatement> Create(
        string title,
        string? remainderOfTitle = null,
        string? statementOfResponsibility = null)
    {
        return string.IsNullOrWhiteSpace(title)
            ? KnResult.Failure<TitleStatement>(BibRecordAggregateErrors.TitleRequired)
            : KnResult.Success(new TitleStatement(title, remainderOfTitle, statementOfResponsibility));
    }

    /// <summary>
    /// Returns the property values used to determine value equality for the TitleStatement.
    /// </summary>
    /// <returns>An enumeration of the Title, RemainderOfTitle, and StatementOfResponsibility values.</returns>
    public override IEnumerable<object?> GetAtomicValues()
    {
        yield return Title;
        yield return RemainderOfTitle;
        yield return StatementOfResponsibility;
    }
}