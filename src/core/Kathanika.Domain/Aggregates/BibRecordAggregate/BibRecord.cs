using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibRecordAggregate;

public sealed class BibRecord : AggregateRoot
{
    public string Title => MarcMetadata.GetDataFieldValue("245", 'a');
    public string Author => MarcMetadata.GetDataFieldValue("100", 'a');
    public string ControlNumber => MarcMetadata.GetControlFieldValue("001");
    public string Isbns => string.Join(", ", MarcMetadata.DataFields
        .Where(df => df.Tag == "020")
        .SelectMany(df => df.Subfields.Where(sf => sf.Code == 'a')
            .Select(sf => sf.Value))
        .Where(v => !string.IsNullOrWhiteSpace(v))
        .Distinct()
        .ToArray());
    public string Issns => string.Join(", ", MarcMetadata.DataFields
        .Where(df => df.Tag == "022")
        .SelectMany(df => df.Subfields.Where(sf => sf.Code == 'a')
            .Select(sf => sf.Value))
        .Where(v => !string.IsNullOrWhiteSpace(v))
        .Distinct()
        .ToArray());
    public string Publisher => MarcMetadata.GetDataFieldValue("260", 'b');
    public int? PublicationYear =>
        MarcMetadata.GetDataFieldValue("260", 'c') is { } yearStr
        && int.TryParse(yearStr, out var year)
            ? year
            : null;

    public string? MaterialType => MarcMetadata.GetMaterialType();
    public string? Note => MarcMetadata.GetDataFieldValue("500", 'a');
    public string? CoverImageId { get; private set; }

    /// <summary>
    /// MARC21 metadata containing complete bibliographic record information.
    /// Stores Leader, Control Fields (001-009), and Data Fields (010-999) according to MARC21 standards.
    /// </summary>
    public MarcMetadata MarcMetadata { get; private set; }

    private BibRecord()
    {
    }

    public KnResult UpdateCoverImage(string coverImageId)
    {
        if (string.IsNullOrWhiteSpace(coverImageId))
            return KnResult.Failure(new KnError("BibRecord.InvalidCoverImageId",
                "Cover image ID cannot be null or empty."));

        CoverImageId = coverImageId;
        return KnResult.Success();
    }

    public KnResult UpdateEdition(string edition)
    {
        if (string.IsNullOrWhiteSpace(edition))
            return KnResult.Failure(new KnError("BibRecord.InvalidEdition",
                "Edition cannot be null or empty."));

        KnResult result = MarcMetadata.AddDataField("250", ' ', ' ',
            [Subfield.Create('a', edition).Value]);

        return result.IsFailure ? KnResult.Failure(result.Errors) : KnResult.Success();
    }

    public KnResult UpdateNote(string note)
    {
        if (string.IsNullOrWhiteSpace(note))
            return KnResult.Failure(new KnError("BibRecord.InvalidNote",
                "Note cannot be null or empty."));

        KnResult result = MarcMetadata.AddDataField("500", ' ', ' ',
            [Subfield.Create('a', note).Value]);

        return result.IsFailure ? KnResult.Failure(result.Errors) : KnResult.Success();
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
        long numberOfPages)
    {
        BibRecord record = new()
        {
            MarcMetadata = MarcMetadata.Create().Value
        };

        record.MarcMetadata.AddDataField("020", ' ', ' ', [Subfield.Create('a', isbn).Value]);
        record.MarcMetadata.AddDataField("041", '0', ' ', [Subfield.Create('a', language).Value]);
        record.MarcMetadata.AddDataField("100", '1', ' ', [Subfield.Create('a', author).Value]);
        record.MarcMetadata.AddDataField("245", '1', '0', [Subfield.Create('a', title).Value]);
        record.MarcMetadata.AddDataField("260", ' ', ' ',
        [
            Subfield.Create('b', publisher).Value,
            Subfield.Create('c', publicationYear.ToString()).Value
        ]);
        record.MarcMetadata.AddDataField("300", ' ', ' ', [Subfield.Create('a', $"{numberOfPages} pages").Value]);

        return KnResult.Success(record);
    }
}