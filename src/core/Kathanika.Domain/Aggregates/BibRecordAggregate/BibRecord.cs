using System.Text;
using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibRecordAggregate;

/// <summary>
/// Represents a bibliographic record that encapsulates metadata such as cataloging information,
/// control numbers, publication details, and language codes for various types of bibliographic material.
/// </summary>
public sealed class BibRecord : AggregateRoot
{
    private List<string> _internationalStandardBookNumbers = [];
    private List<PublicationDistribution> _publicationDistributions = [];
    private List<PhysicalDescription> _physicalDescriptions = [];

    /// <summary>
    ///     000 (Record Leader) - Must be exactly 24 characters.
    /// </summary>
    public string Leader { get; private set; }

    /// <summary>
    ///     001 (Control Number) - Must be unique and less than 50 characters.
    /// </summary>
    public string ControlNumber { get; private set; }

    /// <summary>
    ///     003 (Control Number Identifier) - Must be less than 50 characters.
    /// </summary>
    public string ControlNumberIdentifier { get; private set; } = "KathanikaLMS";

    /// <summary>
    ///     005 (Date and Time of Latest Transaction) - Must be a valid DateTime.
    /// </summary>
    public string DateAndTimeOfLatestTransaction { get; private set; } = DateTime.UtcNow.ToString("yyyyMMddHHmmss.f");

    /// <summary>
    ///     008 (Fixed-Length Data Elements) - Must be exactly 40 characters.
    /// </summary>
    public string FixedLengthDataElements { get; private set; }

    /// <summary>
    /// 020 (International Standard Book Number) - Optional
    /// Contains the International Standard Book Number (ISBN) assigned to the resource.
    /// </summary>
    public IReadOnlyList<string> InternationalStandardBookNumbers
    {
        get => _internationalStandardBookNumbers;
        private set => _internationalStandardBookNumbers = [.. value];
    }

    /// <summary>
    /// Represents the MARC21 Main Entry - Personal Name (Field 100).
    /// Contains information about the main entry of a personal name,
    /// including title, dates associated with the name, and relator term.
    /// </summary>
    public MainEntryPersonalName MainEntryPersonalName { get; private set; }

    /// <summary>
    /// Represents the MARC21 Title Statement (Field 245).
    /// Contains the title and optionally the remainder of the title
    /// and statement of responsibility.
    /// </summary>
    public TitleStatement TitleStatement { get; private set; }

    /// <summary>
    /// Represents the MARC21 Publication, Distribution, etc. (Field 260).
    /// Contains information about the publication, distribution, manufacture,
    /// and copyright notice of a resource.
    /// </summary>
    public IReadOnlyList<PublicationDistribution> PublicationDistributions
    {
        get => _publicationDistributions;
        private set => _publicationDistributions = [.. value];
    }

    /// <summary>
    /// Represents the MARC21 Physical Description (Field 300).
    /// Contains information about the physical characteristics of a resource,
    /// including its extent, dimensions, and other physical details.
    /// </summary>
    public IReadOnlyList<PhysicalDescription> PhysicalDescriptions
    {
        get => _physicalDescriptions;
        private set => _physicalDescriptions = [.. value];
    }

    public string? CoverImageId { get; private set; }


    /// <summary>
    /// Initializes a new instance of the <see cref="BibRecord"/> class with default values.
    /// </summary>
    private BibRecord()
    {
    }

    /// <summary>
    /// Generates a standard MARC21 Leader (000) field with 24 characters.
    /// </summary>
    /// <param name="recordStatus">Record status code (05): n=new, c=corrected, d=deleted, etc.</param>
    /// <param name="recordType">Type of record code (06): a=text, m=computer file, etc.</param>
    /// <param name="bibLevel">Bibliographic level code (07): m=monograph, s=serial, etc.</param>
    /// <param name="controlType">Type of control code (08): a=archival control, etc.</param>
    /// <param name="encodingLevel">Encoding level code (17): 1=full level, 4=core level, etc.</param>
    /// <param name="catalogingForm">Descriptive cataloging form code (18): a=AACR2, i=ISBD, etc.</param>
    /// <returns>A properly formatted 24-character MARC21 Leader field.</returns>
    /// <remarks>
    /// The Leader is the first field in a MARC record and contains 24 character positions (00-23)
    /// with numbers or coded values that define parameters for processing the record.
    /// For detailed format specifications see: https://www.loc.gov/marc/bibliographic/bdleader.html
    /// <summary>
    /// Generates and sets the 24-character MARC21 Leader field for the bibliographic record.
    /// </summary>
    /// <param name="recordStatus">Single-character code indicating the record status (default 'n').</param>
    /// <param name="recordType">Single-character code for the type of record (default 'a').</param>
    /// <param name="bibLevel">Single-character code for the bibliographic level (default 'm').</param>
    /// <param name="controlType">Single-character code for the type of control (default ' ').</param>
    /// <param name="encodingLevel">Single-character code for the encoding level (default ' ').</param>
    /// <param name="catalogingForm">Single-character code for the descriptive cataloging form (default 'i').</param>
    private void GenerateLeader(
        char recordStatus = 'n',
        char recordType = 'a',
        char bibLevel = 'm',
        char controlType = ' ',
        char encodingLevel = ' ',
        char catalogingForm = 'i')
    {
        StringBuilder leader = new("00000"); // Positions 00-04: Record length (filled later by the system)
        leader.Append(recordStatus); // Position 05: Record status
        leader.Append(recordType); // Position 06: Type of record
        leader.Append(bibLevel); // Position 07: Bibliographic level
        leader.Append(controlType); // Position 08: Type of control
        leader.Append('a'); // Position 09: Character coding scheme (a=UTF-8)
        leader.Append('2'); // Position 10: Indicator count
        leader.Append('2'); // Position 11: Subfield code count
        leader.Append("00000"); // Positions 12-16: Base address of data (filled later by the system)
        leader.Append(encodingLevel); // Position 17: Encoding level
        leader.Append(catalogingForm); // Position 18: Descriptive cataloging form
        leader.Append('0'); // Position 19: Multipart resource record level
        leader.Append('4'); // Position 20: Length of the length-of-field portion
        leader.Append('5'); // Position 21: Length of the starting-character-position portion
        leader.Append('0'); // Position 22: Length of the implementation-defined portion
        leader.Append('0'); // Position 23: Undefined

        Leader = leader.ToString();
    }

    /// <summary>
    /// Generates a unique control number (001) field for a bibliographic record.
    /// </summary>
    /// <param name="prefix">Optional organizational prefix for the control number.</param>
    /// <returns>A unique control number string.</returns>
    /// <remarks>
    /// Field 001 contains the control number assigned by the organization creating, using, or distributing the record.
    /// For detailed format specifications see: https://www.loc.gov/marc/bibliographic/bd001.html
    /// <summary>
    /// Generates and assigns a unique control number using a fixed prefix, the current UTC timestamp, and a GUID segment.
    /// </summary>
    private void GenerateControlNumber()
    {
        // Use a combination of prefix, timestamp, and GUID to ensure uniqueness
        var timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmmss");
        var uniquePart = Guid.NewGuid().ToString("N")[..8];

        ControlNumber = $"KN{timestamp}{uniquePart}";
    }

    /// <summary>
    /// Gets material-specific data for positions 18-34 of the 008 field based on material type.
    /// </summary>
    /// <param name="materialType">The type of material being cataloged.</param>
    /// <summary>
    /// Returns the MARC21 material-specific 17-character string for positions 18-34 of the 008 field based on the given material type.
    /// </summary>
    /// <param name="materialType">The type of material for which to generate the data.</param>
    /// <returns>A 17-character string for positions 18-34 of the 008 field.</returns>
    private static string GetMaterialSpecificData(MaterialType materialType)
    {
        // Default values for different material types
        return materialType switch
        {
            MaterialType.Book => "i      00010 eng d", // Book-specific data
            MaterialType.Serial => "           0   0", // Serial-specific data
            MaterialType.Map => "a     1   0   e", // Map-specific data
            MaterialType.VisualMaterial => "             ", // Visual material-specific data
            MaterialType.MixedMaterial => "             ", // Mixed material-specific data
            MaterialType.ComputerFile => "cr una       ", // Computer file-specific data
            MaterialType.Music => "    n        ", // Music-specific data
            _ => "             ", // Default to blank for unknown material types
        };
    }

    /// <summary>
    /// Generates fixed-length data elements (008) field with 40 characters based on material type.
    /// </summary>
    /// <param name="publicationDate">Publication date in YYYY format.</param>
    /// <param name="publicationPlace">Publication place code (2 characters, e.g., "us" for United States).</param>
    /// <param name="language">Language code (3 characters, e.g., "eng" for English).</param>
    /// <param name="materialType">Type of material (book, serial, map, etc.).</param>
    /// <returns>A properly formatted 40-character 008 field string.</returns>
    /// <remarks>
    /// Field 008 contains 40 character positions (00-39) that provide coded information about the record as a whole
    /// and about special bibliographic aspects of the item being cataloged.
    /// For detailed format specifications see: https://www.loc.gov/marc/bibliographic/bd008.html
    /// <summary>
    /// Generates and sets the 40-character MARC21 fixed-length data elements (field 008) for the bibliographic record.
    /// </summary>
    /// <param name="publicationDate">The publication year (4 digits) or empty if unknown.</param>
    /// <param name="publicationPlace">The MARC21 place of publication code (default "xx").</param>
    /// <param name="language">The MARC21 language code (default "xxx").</param>
    /// <param name="materialType">The material type used to determine material-specific data fields.</param>
    private void GenerateFixedLengthDataElements(
        string publicationDate = "",
        string publicationPlace = "xx",
        string language = "xxx",
        MaterialType materialType = MaterialType.Book)
    {
        StringBuilder fixedLengthDataElements = new();
        fixedLengthDataElements.Append(DateTime.UtcNow.ToString("yyMMdd")); // Positions 00-05: Date entered on file
        fixedLengthDataElements.Append('s'); // Position 06: Publication status (s = single known date)
        fixedLengthDataElements.Append(string.IsNullOrEmpty(publicationDate)
            ? "    "
            : publicationDate.PadRight(4)); // Positions 07-10: Publication date 1
        fixedLengthDataElements.Append("    "); // Positions 11-14: Publication date 2 (blank for single date)
        fixedLengthDataElements.Append(publicationPlace); // Positions 15-17: Publication place
        fixedLengthDataElements.Append(
            GetMaterialSpecificData(materialType)); // Positions 18-34: Material-specific data
        fixedLengthDataElements.Append(language.PadRight(3)); // Positions 35-37: Language code
        fixedLengthDataElements.Append(' '); // Position 38: Modified record (not modified)
        fixedLengthDataElements.Append('d');


        FixedLengthDataElements = fixedLengthDataElements.ToString();
    }


    /// <summary>
    /// Creates a new <see cref="BibRecord"/> instance for a book with the specified bibliographic details.
    /// </summary>
    /// <param name="title">The title of the book. Must be valid according to <see cref="TitleStatement"/> validation.</param>
    /// <param name="isbn">The International Standard Book Number (ISBN) of the book, if available.</param>
    /// <param name="personalName">The main entry personal name (author) of the book, if available.</param>
    /// <param name="publisherName">The name of the publisher, if available.</param>
    /// <param name="publicationDate">The publication date, if available.</param>
    /// <param name="extent">The physical extent (e.g., number of pages), if available.</param>
    /// <param name="dimensions">The physical dimensions of the book, if available.</param>
    /// <param name="coverImageId">The identifier for the cover image, if available.</param>
    /// <returns>
    /// A <see cref="KnResult{BibRecord}"/> containing the created <see cref="BibRecord"/> if successful,
    /// or a failure result with validation errors if the title is invalid.
    /// </returns>
    public static KnResult<BibRecord> CreateBookRecord(
        string title,
        string? isbn,
        string? personalName,
        string? publisherName,
        string? publicationDate,
        string? extent,
        string? dimensions,
        string? coverImageId)
    {
        List<KnError> errors = [];

        KnResult<TitleStatement> titleStatementResult = TitleStatement.Create(title);
        if (titleStatementResult.IsFailure)
            errors.AddRange(titleStatementResult.Errors);

        if (errors.Count != 0)
            return KnResult.Failure<BibRecord>(errors);

        BibRecord record = new()
        {
            TitleStatement = titleStatementResult.Value,
            CoverImageId = coverImageId,
        };

        if (!string.IsNullOrWhiteSpace(isbn))
            record.InternationalStandardBookNumbers = [isbn];

        if (!string.IsNullOrWhiteSpace(personalName))
            record.MainEntryPersonalName = new MainEntryPersonalName(personalName);

        if (!string.IsNullOrWhiteSpace(publisherName) || !string.IsNullOrWhiteSpace(publicationDate))
            record.PublicationDistributions =
            [
                new PublicationDistribution(publisherName, publicationDate)
            ];

        if (!string.IsNullOrWhiteSpace(extent) || !string.IsNullOrWhiteSpace(dimensions))
            record.PhysicalDescriptions =
            [
                new PhysicalDescription(extent, dimensions)
            ];

        return KnResult.Success(record);
    }
}