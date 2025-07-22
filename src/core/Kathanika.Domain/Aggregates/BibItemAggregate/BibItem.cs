using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibItemAggregate;

public class BibItem : AggregateRoot
{
    public string BibRecordId { get; private init; }
    public string Barcode { get; private set; } = string.Empty;
    public string CallNumber { get; private set; } = string.Empty;
    public string Location { get; private set; } = string.Empty;
    public ItemType ItemType { get; private set; }
    public ItemVendor? Vendor { get; private set; }
    public DateOnly? AcquisitionDate { get; private set; }
    public AcquisitionType AcquisitionType { get; private set; } = AcquisitionType.Purchase;
    public ItemStatus Status { get; private set; } = ItemStatus.Available;
    public DateTime? LastCheckOutDate { get; private set; }
    public DateTime? LastCheckInDate { get; private set; }
    public string? ConditionNote { get; private set; }
    public string? Notes { get; private set; }
    public DateTime? WithdrawnDate { get; private set; }

    private BibItem()
    {
    }

    private BibItem(
        string bibRecordId,
        string barcode,
        string callNumber,
        string location,
        ItemType itemType,
        ItemStatus status,
        ItemVendor? vendor = null,
        DateOnly? acquisitionDate = null,
        AcquisitionType acquisitionType = AcquisitionType.Purchase,
        string? conditionNote = null,
        string? notes = null)
    {
        BibRecordId = bibRecordId;
        Barcode = barcode;
        CallNumber = callNumber;
        Location = location;
        ItemType = itemType;
        Status = status;
        Vendor = vendor;
        AcquisitionDate = acquisitionDate ?? DateOnly.FromDateTime(DateTime.Today);
        AcquisitionType = acquisitionType;
        ConditionNote = conditionNote;
        Notes = notes;
    }

    public static BibItem Create(
        string bibRecordId,
        string barcode,
        string callNumber,
        string location,
        ItemType itemType,
        ItemStatus status,
        ItemVendor? vendor = null,
        DateOnly? acquisitionDate = null,
        AcquisitionType acquisitionType = AcquisitionType.Purchase,
        string? conditionNote = null,
        string? notes = null)
    {
        return new BibItem(
            bibRecordId,
            barcode,
            callNumber,
            location,
            itemType,
            status,
            vendor,
            acquisitionDate,
            acquisitionType,
            conditionNote,
            notes);
    }
}