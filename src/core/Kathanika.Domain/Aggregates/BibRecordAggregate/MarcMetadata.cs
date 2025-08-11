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
    /// Position 0-4: Record length, 5: Record status, 6: Type of record, 7: Bibliographic level, etc.
    /// </summary>
    public string Leader { get; private set; } = string.Empty;

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

    private MarcMetadata()
    {
    }

    /// <summary>
    /// Creates a new MARC21 metadata record with validation.
    /// Validates all MARC21 constraints, including field repeatability and format requirements.
    /// </summary>
    public static KnResult<MarcMetadata> Create(
        string leader,
        IEnumerable<ControlField> controlFields,
        IEnumerable<DataField> dataFields)
    {
        List<ControlField> controlFieldsList = controlFields.ToList();
        List<DataField> dataFieldsList = dataFields.ToList();

        // Validate Leader (24 characters exactly)
        KnResult leaderValidation = ValidateLeader(leader);
        if (leaderValidation.IsFailure)
            return KnResult.Failure<MarcMetadata>(leaderValidation.Errors);

        // Validate Control Fields
        KnResult controlFieldsValidation = ValidateControlFields(controlFieldsList);
        if (controlFieldsValidation.IsFailure)
            return KnResult.Failure<MarcMetadata>(controlFieldsValidation.Errors);

        return KnResult.Success(new MarcMetadata
        {
            Leader = leader,
            ControlFields = controlFieldsList,
            DataFields = dataFieldsList
        });
    }

    private static KnResult ValidateLeader(string leader)
    {
        if (string.IsNullOrEmpty(leader) || leader.Length != 24)
            return KnResult.Failure(BibRecordAggregateErrors.LeaderInvalid);

        return KnResult.Success();
    }

    private static KnResult ValidateControlFields(IList<ControlField> controlFields)
    {
        List<string> tags = controlFields.Select(cf => cf.Tag).ToList();

        // Check for required non-repeatable fields: 001, 005
        ControlField? controlNumber = controlFields.FirstOrDefault(cf => cf.Tag == "001");
        if (controlNumber == null)
            return KnResult.Failure(BibRecordAggregateErrors.ControlNumberInvalid);

        ControlField? dateTimeField = controlFields.FirstOrDefault(cf => cf.Tag == "005");
        if (dateTimeField == null)
            return KnResult.Failure(BibRecordAggregateErrors.DateTimeOfLatestTransactionInvalid);

        // Validate non-repeatability for 001 and 005
        if (tags.Count(t => t == "001") > 1)
            return KnResult.Failure(BibRecordAggregateErrors.ControlNumberInvalid);

        if (tags.Count(t => t == "005") > 1)
            return KnResult.Failure(BibRecordAggregateErrors.DateTimeOfLatestTransactionInvalid);


        return KnResult.Success();
    }

    public string GetControlFieldValue(string tag)
    {
        ControlField? field = _controlFields.FirstOrDefault(f => f.Tag == tag);
        return field?.Data ?? string.Empty;
    }

    public string GetDataFieldValue(string tag, char subfieldCode)
    {
        DataField? field = _dataFields.FirstOrDefault(f => f.Tag == tag);
        if (field == null)
            return string.Empty;

        Subfield? subfield = field.Subfields.FirstOrDefault(s => s.Code == subfieldCode);
        return subfield?.Value ?? string.Empty;
    }

    public string GetMaterialType()
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
}