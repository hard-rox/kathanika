using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibRecordAggregate;

/// <summary>
/// MARC21 (Machine-Readable Cataloging) metadata value object for bibliographic records.
/// Implements complete MARC21 structure with Leader, Control Fields, and Data Fields.
/// Follows MARC21 standards for library cataloging with full validation.
/// </summary>
public class MarcMetadata : Entity
{
    private readonly List<ControlField> _controlFields = [];
    private readonly List<DataField> _dataFields = [];

    /// <summary>
    /// MARC21 Leader - Fixed-length 24-character field containing metadata about the record.
    /// Position 0-4: Record length, 5: Record status, 6: Type of record, 7: Bibliographic level, 8: Encoding level,
    /// 9: Descriptive cataloging form, 10-11: Character coding scheme,
    /// 12-16: Base address of data, 17-23: Undefined.
    /// </summary>
    public string Leader { get; private set; } = new(' ', 24);

    /// <summary>
    /// Control Fields (001-009) - Variable-length fields without indicators or subfields.
    /// Key fields include: 001 (Control Number), 003 (Control Number Identifier), 
    /// 005 (Date/Time of Latest Transaction), 008 (Fixed-Length Data Elements).
    /// </summary>
    public IReadOnlyList<ControlField> ControlFields
    {
        get => _controlFields;
        private init => _controlFields = value.ToList();
    }

    /// <summary>
    /// Data Fields (010-999) - Variable-length fields with indicators and subfields.
    /// Contains bibliographic data like titles, authors, subjects, etc.
    /// </summary>
    public IReadOnlyList<DataField> DataFields
    {
        get => _dataFields;
        private init => _dataFields = value.ToList();
    }

    private string RecalculateLeader()
    {
        // Calculate total record length
        var recordLength = CalculateRecordLength();

        // Get current leader as a char array for modification
        var leader = Leader.ToCharArray();

        // Update record length (positions 0-4)
        var lengthString = recordLength.ToString("D5");
        for (var i = 0; i < 5; i++)
        {
            leader[i] = lengthString[i];
        }

        // Calculate the base address of data (positions 12-16)
        var baseAddress = CalculateBaseAddress();
        var baseAddressString = baseAddress.ToString("D5");
        for (var i = 0; i < 5; i++)
        {
            leader[12 + i] = baseAddressString[i];
        }

        return new string(leader);
    }

    private int CalculateRecordLength()
    {
        // Leader: 24 bytes
        var length = 24;

        // Directory entries: 12 bytes per field (control + data fields)
        var totalFields = _controlFields.Count + _dataFields.Count;
        length += totalFields * 12;

        // Directory terminator: 1 byte
        length += 1;

        // Control fields data
        length += _controlFields.Sum(field => field.Data.Length + 1);

        // Data fields data
        foreach (DataField field in _dataFields)
        {
            length += 2; // indicators
            foreach (Subfield subfield in field.Subfields)
            {
                length += 1; // subfield delimiter
                length += 1; // subfield code
                length += subfield.Value.Length;
            }

            length += 1; // field terminator
        }

        // Record terminator: 1 byte
        length += 1;

        return length;
    }

    private int CalculateBaseAddress()
    {
        // Leader: 24 bytes
        var baseAddress = 24;

        // Directory entries: 12 bytes per field
        var totalFields = _controlFields.Count + _dataFields.Count;
        baseAddress += totalFields * 12;

        // Directory terminator: 1 byte
        baseAddress += 1;

        return baseAddress;
    }

    /// <summary>
    /// Generates MARC21 008 field (Fixed-Length Data Elements) for books.
    /// 008 field contains 40 positions with coded data elements for various bibliographic aspects.
    /// </summary>
    /// <param name="dateOfCreation">Date of creation (positions 0-5)</param>
    /// <param name="publicationYear">Year of publication (positions 7-10)</param>
    /// <param name="language">Language code (positions 35-37)</param>
    /// <returns>40-character 008 field value</returns>
    private static string GenerateFixedLengthDataElements(DateTime? dateOfCreation = null,
        string? publicationYear = null, string? language = null)
    {
        var field008 = new char[40];

        // Initialize with spaces
        for (var i = 0; i < 40; i++)
        {
            field008[i] = ' ';
        }

        SetDateEntered(field008, dateOfCreation ?? DateTime.UtcNow);
        SetPublicationData(field008, publicationYear);
        SetPublicationPlace(field008);
        SetContentAttributes(field008);
        SetLanguage(field008, language);
        SetRecordAttributes(field008);

        return new string(field008);
    }

    private static void SetDateEntered(char[] field008, DateTime creationDate)
    {
        // Positions 0-5: Date entered on file (YYMMDD)
        var dateEntered = creationDate.ToString("yyMMdd");
        for (var i = 0; i < 6; i++)
        {
            field008[i] = dateEntered[i];
        }

        // Position 6: Type of date/Publication status
        field008[6] = 's'; // Single known date
    }

    private static void SetPublicationData(char[] field008, string? publicationYear)
    {
        // Positions 7-10: Date 1 (Publication year)
        if (!string.IsNullOrEmpty(publicationYear) && publicationYear.Length == 4)
        {
            for (var i = 0; i < 4; i++)
            {
                field008[7 + i] = publicationYear[i];
            }
        }
        else
        {
            // Unknown date
            for (var i = 7; i <= 10; i++)
            {
                field008[i] = 'u';
            }
        }

        // Positions 11-14: Date 2 (blank for single date)
        for (var i = 11; i <= 14; i++)
        {
            field008[i] = ' ';
        }
    }

    private static void SetPublicationPlace(char[] field008)
    {
        // Position 15-17: Place of publication (XXX for unknown)
        field008[15] = 'x';
        field008[16] = 'x';
        field008[17] = ' ';
    }

    private static void SetContentAttributes(char[] field008)
    {
        // Positions 18-21: Illustrations (blank = no illustrations)
        for (var i = 18; i <= 21; i++)
        {
            field008[i] = ' ';
        }

        // Position 22: Target audience
        field008[22] = ' '; // Unknown or not specified

        // Position 23: Form of item
        field008[23] = ' '; // None of the following

        // Positions 24-27: Nature of contents
        for (var i = 24; i <= 27; i++)
        {
            field008[i] = ' ';
        }

        // Position 28: Government publication
        field008[28] = ' '; // Not a government publication

        // Position 29: Conference publication
        field008[29] = '0'; // Not a conference publication

        // Position 30: Festschrift
        field008[30] = '0'; // Not a festschrift

        // Position 31: Index
        field008[31] = '0'; // No index

        // Position 32: Undefined
        field008[32] = ' ';

        // Position 33: Literary form
        field008[33] = '0'; // Not fiction

        // Position 34: Biography
        field008[34] = ' '; // No biographical material
    }

    private static void SetLanguage(char[] field008, string? language)
    {
        // Positions 35-37: Language
        var lang = language?.ToLowerInvariant() ?? "eng";
        if (lang.Length >= 3)
        {
            for (var i = 0; i < 3; i++)
            {
                field008[35 + i] = lang[i];
            }
        }
        else
        {
            field008[35] = 'e';
            field008[36] = 'n';
            field008[37] = 'g';
        }
    }

    private static void SetRecordAttributes(char[] field008)
    {
        // Position 38: Modified record
        field008[38] = ' '; // Not modified

        // Position 39: Cataloging source
        field008[39] = 'd'; // Other
    }

    private MarcMetadata()
    {
    }

    internal static KnResult<MarcMetadata> Create()
    {
        MarcMetadata metadata = new();

        metadata._controlFields.AddRange([
            ControlField.Create("001", "KN" + Guid.NewGuid().ToString("N")[..10].ToUpper()).Value,
            ControlField.Create("005", DateTime.UtcNow.ToString("yyyyMMddHHmmss.f")).Value,
            ControlField.Create("008", GenerateFixedLengthDataElements()).Value,
        ]);

        metadata.Leader = metadata.RecalculateLeader();
        return KnResult.Success(metadata);
    }

    internal string GetControlFieldValue(string tag)
    {
        ControlField? field = _controlFields.FirstOrDefault(f => f.Tag == tag);
        return field?.Data ?? string.Empty;
    }

    internal string GetDataFieldValue(string tag, char subfieldCode)
    {
        DataField? field = _dataFields.FirstOrDefault(f => f.Tag == tag);
        if (field == null)
            return string.Empty;

        Subfield? subfield = field.Subfields.FirstOrDefault(s => s.Code == subfieldCode);
        return subfield?.Value ?? string.Empty;
    }

    internal string GetMaterialType()
    {
        var controlField008Value = GetControlFieldValue("008");
        if (controlField008Value.Length < 7)
            return "Unknown";
        return controlField008Value[6] switch
        {
            'a' => "Books",
            'b' => "Continuing Resources",
            'c' => "Computer Files",
            'd' => "Visual Materials",
            'e' => "Sound Recordings",
            'f' => "Mixed Materials",
            'g' => "Manuscripts",
            _ => "Unknown"
        };
    }

    internal KnResult AddDataField(string tag, char indicator1, char indicator2, IEnumerable<Subfield> subfields)
    {
        KnResult<DataField> dataFieldResult = DataField.Create(tag, indicator1, indicator2, subfields);
        if (dataFieldResult.IsFailure)
            return KnResult.Failure(dataFieldResult.Errors);

        _dataFields.Add(dataFieldResult.Value);
        Leader = RecalculateLeader();

        return KnResult.Success();
    }
}