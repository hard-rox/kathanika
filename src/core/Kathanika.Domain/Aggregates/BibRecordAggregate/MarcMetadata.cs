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
    public string Leader { get; private set; }

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

    private MarcMetadata()
    {
    }

    internal static KnResult<MarcMetadata> Create()
    {
        MarcMetadata metadata = new();

        metadata._controlFields.AddRange([
            ControlField.Create("001", "KN" + Guid.NewGuid().ToString("N")[..10].ToUpper()).Value,
            ControlField.Create("005", DateTime.UtcNow.ToString("yyyyMMddHHmmss.f")).Value
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
        if (controlField008Value.Length < 6)
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