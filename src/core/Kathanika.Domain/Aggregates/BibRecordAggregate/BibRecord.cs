using HotChocolate;
using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibRecordAggregate;

public sealed class BibRecord : AggregateRoot
{
    public string Title => MarcMetadata?.GetDataFieldValue("245", 'a') ?? string.Empty;
    public string Author => MarcMetadata?.GetDataFieldValue("100", 'a') ?? string.Empty;
    public string ControlNumber => MarcMetadata?.GetControlFieldValue("001") ?? string.Empty;
    public string CallNumber => MarcMetadata?.GetDataFieldValue("050", 'a') ?? string.Empty;
    public string Isbn => MarcMetadata?.GetDataFieldValue("020", 'a') ?? string.Empty;
    public string Issn => MarcMetadata?.GetDataFieldValue("022", 'a') ?? string.Empty;
    public string Publisher => MarcMetadata?.GetDataFieldValue("260", 'b') ?? string.Empty;

    public int? PublicationYear =>
        MarcMetadata?.GetDataFieldValue("260", 'c') is { } yearStr
        && int.TryParse(yearStr, out var year)
            ? year
            : null;

    public string? MaterialType => MarcMetadata?.GetMaterialType();
    public string? Note => MarcMetadata?.GetDataFieldValue("500", 'a');
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
            CoverImageId = coverImageId
        };

        // Generate MARC metadata from provided parameters
        KnResult<MarcMetadata> marcResult = CreateMarcMetadataForBook(
            title, author, isbn, publisher, publicationYear,
            language, numberOfPages, edition, note);

        if (marcResult.IsFailure)
            return KnResult.Failure<BibRecord>(marcResult.Errors);

        record.MarcMetadata = marcResult.Value;

        return KnResult.Success(record);
    }

    private static KnResult<MarcMetadata> CreateMarcMetadataForBook(
        string title,
        string author,
        string isbn,
        string publisher,
        int publicationYear,
        string language,
        long numberOfPages,
        string edition,
        string? note)
    {
        DateTime now = DateTime.UtcNow;
        var controlNumber = Guid.NewGuid().ToString("N")[..10].ToUpper();

        // Create control fields
        List<ControlField> controlFields =
        [
            ControlField.Create("001", controlNumber).Value,
            ControlField.Create("005", now.ToString("yyyyMMddHHmmss.f")).Value,
            ControlField.Create("008", CreateField008(publicationYear, language)).Value
        ];

        // Create data fields
        List<DataField> dataFields = [];

        // 020 - ISBN
        if (!string.IsNullOrWhiteSpace(isbn))
        {
            KnResult<DataField> isbnResult = DataField.Create("020", ' ', ' ',
                [Subfield.Create('a', isbn).Value]);
            if (isbnResult.IsSuccess)
                dataFields.Add(isbnResult.Value);
        }

        // 100 - Main Entry - Personal Name (Author)
        KnResult<DataField> authorResult = DataField.Create("100", '1', ' ',
            [Subfield.Create('a', author).Value]);
        if (authorResult.IsSuccess)
            dataFields.Add(authorResult.Value);

        // 245 - Title Statement
        KnResult<DataField> titleResult = DataField.Create("245", '1', '0',
            [Subfield.Create('a', title).Value]);
        if (titleResult.IsSuccess)
            dataFields.Add(titleResult.Value);

        // 250 - Edition Statement
        if (!string.IsNullOrWhiteSpace(edition))
        {
            KnResult<DataField> editionResult = DataField.Create("250", ' ', ' ',
                [Subfield.Create('a', edition).Value]);
            if (editionResult.IsSuccess)
                dataFields.Add(editionResult.Value);
        }

        // 260 - Publication, Distribution, etc.
        KnResult<DataField> publicationResult = DataField.Create("260", ' ', ' ',
        [
            Subfield.Create('b', publisher).Value,
            Subfield.Create('c', publicationYear.ToString()).Value
        ]);
        if (publicationResult.IsSuccess)
            dataFields.Add(publicationResult.Value);

        // 300 - Physical Description
        KnResult<DataField> physicalResult = DataField.Create("300", ' ', ' ',
            [Subfield.Create('a', $"{numberOfPages} pages").Value]);
        if (physicalResult.IsSuccess)
            dataFields.Add(physicalResult.Value);

        // 500 - General Note
        if (!string.IsNullOrWhiteSpace(note))
        {
            KnResult<DataField> noteResult = DataField.Create("500", ' ', ' ',
                [Subfield.Create('a', note).Value]);
            if (noteResult.IsSuccess)
                dataFields.Add(noteResult.Value);
        }

        // Create MARC metadata
        return MarcMetadata.Create(
            "00000nam a2200000 a 4500", // Standard leader for books
            controlFields,
            dataFields);
    }

    private static string CreateField008(int publicationYear, string language)
    {
        var dateEntered = DateTime.UtcNow.ToString("yyMMdd");
        var typeOfDate = "s"; // Single known date
        var date1 = publicationYear.ToString().PadLeft(4, '0');
        var date2 = "    "; // Blank for single date
        var placeOfPublication = "   "; // Unknown
        var languageCode = language.Length >= 3 ? language[..3].ToLower() : language.PadRight(3);
        var modifiedRecord = " "; // Not modified

        return
            $"{dateEntered}{typeOfDate}{date1}{date2}{placeOfPublication}{languageCode}     000 0 {languageCode} {modifiedRecord}";
    }
}