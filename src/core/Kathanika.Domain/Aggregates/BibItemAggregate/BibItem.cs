using System.Text.RegularExpressions;
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
    public AcquisitionType AcquisitionType { get; private set; } = AcquisitionType.Transfer;
    public ItemStatus Status { get; private set; } = ItemStatus.Available;
    public DateTime? LastCheckOutDate { get; private set; }
    public DateTime? LastCheckInDate { get; private set; }
    public string? ConditionNote { get; private set; }
    public string? Notes { get; private set; }
    public DateTime? WithdrawnDate { get; private set; }

#pragma warning disable CS8618, CS9264
    // ReSharper disable once UnusedMember.Local
    private BibItem()
    {
    }
#pragma warning restore CS8618, CS9264

    private BibItem(
        string bibRecordId,
        string barcode,
        string callNumber,
        string location,
        ItemType itemType,
        ItemStatus status)
    {
        BibRecordId = bibRecordId;
        Barcode = barcode;
        CallNumber = callNumber;
        Location = location;
        ItemType = itemType;
        Status = status;
    }

    public static KnResult<BibItem> Create(
        string bibRecordId,
        string barcode,
        string callNumber,
        string location,
        ItemType itemType,
        ItemStatus status = ItemStatus.Available,
        string? conditionNote = null,
        string? notes = null,
        ItemVendor? vendor = null,
        AcquisitionType acquisitionType = AcquisitionType.Transfer,
        DateOnly? acquisitionDate = null)
    {
        List<KnError> errors = [];

        if (string.IsNullOrWhiteSpace(bibRecordId))
            errors.Add(BibItemAggregateErrors.BibRecordIdIsEmpty);

        if (string.IsNullOrWhiteSpace(barcode))
            errors.Add(BibItemAggregateErrors.BarcodeIsEmpty);

        if (string.IsNullOrWhiteSpace(callNumber))
            errors.Add(BibItemAggregateErrors.CallNumberIsEmpty);

        if (string.IsNullOrWhiteSpace(location))
            errors.Add(BibItemAggregateErrors.LocationIsEmpty);

        if (errors.Count > 0)
            return KnResult.Failure<BibItem>(errors);

        BibItem newBibItem = new(
            bibRecordId,
            barcode,
            callNumber,
            location,
            itemType,
            status)
        {
            ConditionNote = conditionNote,
            Notes = notes,
            Vendor = vendor,
            AcquisitionType = acquisitionType,
            AcquisitionDate = acquisitionDate
        };

        return KnResult.Success(newBibItem);
    }

    public KnResult Update(
        string? barcode = null,
        string? callNumber = null,
        string? location = null,
        ItemType? itemType = null,
        string? conditionNote = null,
        string? notes = null)
    {
        List<KnError> errors = [];

        if (barcode is not null && string.IsNullOrWhiteSpace(barcode))
            errors.Add(BibItemAggregateErrors.BarcodeIsEmpty);

        if (callNumber is not null && string.IsNullOrWhiteSpace(callNumber))
            errors.Add(BibItemAggregateErrors.CallNumberIsEmpty);

        if (location is not null && string.IsNullOrWhiteSpace(location))
            errors.Add(BibItemAggregateErrors.LocationIsEmpty);

        if (errors.Count > 0)
            return KnResult.Failure(errors);

        Barcode = barcode ?? Barcode;
        CallNumber = callNumber ?? CallNumber;
        Location = location ?? Location;
        ItemType = itemType ?? ItemType;
        ConditionNote = conditionNote ?? ConditionNote;
        Notes = notes ?? Notes;

        return KnResult.Success();
    }
    
    public KnResult CheckOut()
    {
        if (Status != ItemStatus.Available)
        {
            return KnResult.Failure(BibItemAggregateErrors.InvalidStatus);
        }
        
        Status = ItemStatus.CheckedOut;
        LastCheckOutDate = DateTime.UtcNow;
        
        return KnResult.Success();
    }

    public KnResult CheckIn()
    {
        if (Status != ItemStatus.CheckedOut)
        {
            return KnResult.Failure(BibItemAggregateErrors.InvalidStatus);
        }
        
        Status = ItemStatus.Available;
        LastCheckInDate = DateTime.UtcNow;
        
        return KnResult.Success();
    }

    public KnResult Withdraw(string? reason = null)
    {
        if (Status == ItemStatus.Withdrawn)
        {
            return KnResult.Failure(BibItemAggregateErrors.AlreadyWithdrawn);
        }

        Status = ItemStatus.Withdrawn;
        WithdrawnDate = DateTime.UtcNow;
        
        if (!string.IsNullOrWhiteSpace(reason))
        {
            Notes = string.IsNullOrEmpty(Notes) ? $"Withdrawn: {reason}" : $"{Notes}\nWithdrawn: {reason}";
        }
        
        return KnResult.Success();
    }

    public KnResult UpdateStatus(ItemStatus newStatus)
    {
        // Add business logic for status transitions if needed
        if (Status == ItemStatus.Withdrawn && newStatus != ItemStatus.Withdrawn)
        {
            return KnResult.Failure(BibItemAggregateErrors.InvalidStatus);
        }

        Status = newStatus;
        return KnResult.Success();
    }
}