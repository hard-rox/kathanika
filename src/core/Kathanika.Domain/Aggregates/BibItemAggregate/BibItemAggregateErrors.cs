using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.BibItemAggregate;

public static class BibItemAggregateErrors
{
    public static readonly KnError NotFound = new(
        "BibItem.NotFound",
        "BibItem not found");

    public static readonly KnError InvalidStatus = new(
        "BibItem.InvalidStatus",
        "The provided status is invalid for this operation");

    public static readonly KnError AlreadyWithdrawn = new(
        "BibItem.AlreadyWithdrawn",
        "This item has already been withdrawn");

    public static readonly KnError BibRecordIdIsEmpty = new(
        "BibItem.BibRecordIdIsEmpty",
        "BibRecord not found");

    public static readonly KnError BarcodeIsEmpty = new(
        "BibItem.BarcodeIsEmpty",
        "Barcode cannot be empty");

    public static readonly KnError CallNumberIsEmpty = new(
        "BibItem.CallNumberIsEmpty",
        "Call number cannot be empty");

    public static readonly KnError LocationIsEmpty = new(
        "BibItem.LocationIsEmpty",
        "Location cannot be empty");

    public static readonly KnError ItemNotAvailableForCheckOut = new(
        "BibItem.ItemNotAvailableForCheckOut",
        "This item is not available for check out");
}