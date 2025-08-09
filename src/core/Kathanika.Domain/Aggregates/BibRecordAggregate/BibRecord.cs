using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibRecordAggregate;

public sealed class BibRecord : AggregateRoot
{
    public string Title { get; private set; }
    public string Author { get; private set; }
    public string? Note { get; private set; }
    public string? CoverImageId { get; private set; }

    /// <summary>
    /// MARC21 metadata containing complete bibliographic record information.
    /// Stores Leader, Control Fields (001-009), and Data Fields (010-999) according to MARC21 standards.
    /// </summary>
    public MarcMetadata? MarcMetadata { get; private set; }

    private BibRecord()
    {
    }

    /// <summary>
    /// Creates a book record with basic metadata (non-MARC21).
    /// For quick cataloging without full MARC21 compliance.
    /// </summary>
    public static KnResult<BibRecord> CreateBookRecord(
        string title,
        string author,
        string isbn,
        string publisher,
        int publicationYear,
        string language,
        long numberOfPages,
        string edition,
        string? note,
        string? coverImageId)
    {
        BibRecord record = new()
        {
            Title = title,
            Author = author,
            Note = note,
            CoverImageId = coverImageId
        };

        return KnResult.Success(record);
    }
}