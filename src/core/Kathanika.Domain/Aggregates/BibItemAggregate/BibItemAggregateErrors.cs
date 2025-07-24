using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibItemAggregate;

public static class BibItemAggregateErrors
{
    public static readonly KnError NotFound = new(
        "BibItem.NotFound",
        "BibItem not found");

    public static readonly KnError BarcodeAlreadyExists = new(
        "BibItem.BarcodeAlreadyExists",
        "A BibItem with this barcode already exists");

    public static readonly KnError InvalidBibRecordId = new(
        "BibItem.InvalidBibRecordId",
        "The provided BibRecord ID is invalid");

    public static readonly KnError InvalidStatus = new(
        "BibItem.InvalidStatus",
        "The provided status is invalid for this operation");

    public static readonly KnError AlreadyWithdrawn = new(
        "BibItem.AlreadyWithdrawn",
        "This item has already been withdrawn");

    public static readonly KnError InvalidVendor = new(
        "BibItem.InvalidVendor",
        "The provided vendor is invalid or does not exist");

    // New validation errors
    public static readonly KnError BibRecordIdIsEmpty = new(
        "BibItem.BibRecordIdIsEmpty",
        "BibRecord ID cannot be empty");

    public static readonly KnError BarcodeIsEmpty = new(
        "BibItem.BarcodeIsEmpty",
        "Barcode cannot be empty");

    public static readonly KnError CallNumberIsEmpty = new(
        "BibItem.CallNumberIsEmpty",
        "Call number cannot be empty");

    public static readonly KnError LocationIsEmpty = new(
        "BibItem.LocationIsEmpty",
        "Location cannot be empty");

    public static readonly KnError InvalidBarcode = new(
        "BibItem.InvalidBarcode",
        "Barcode format is invalid");

    public static readonly KnError InvalidCallNumber = new(
        "BibItem.InvalidCallNumber",
        "Call number format is invalid");
}