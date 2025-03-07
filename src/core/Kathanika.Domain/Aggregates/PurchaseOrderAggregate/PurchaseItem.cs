using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates.PurchaseOrderAggregate;

public sealed class PurchaseItem : Entity
{
    private PurchaseItem()
    {
    }
    public string Title { get; private set; }
    public string? Author { get; private set; }
    public string? Publisher { get; private set; }
    public string? Edition { get; private set; }
    public int? PublishingYear { get; private set; }
    public string? Isbn { get; private set; }
    public int Quantity { get; private set; }
    public decimal? VendorPrice { get; private set; }
    public string? InternalNote { get; private set; }
    public string? VendorNote { get; private set; }

    private PurchaseItem(
        string title,
        int quantity,
        string? author,
        string? publisher,
        string? edition,
        int? publishingYear,
        string? isbn,
        decimal? vendorPrice,
        string? internalNote,
        string? vendorNote)
    {
        Title = title;
        Quantity = quantity;
        Author = author;
        Publisher = publisher;
        Edition = edition;
        PublishingYear = publishingYear;
        Isbn = isbn;
        VendorPrice = vendorPrice;
        InternalNote = internalNote;
        VendorNote = vendorNote;
    }

    public static PurchaseItem Create(
        string title,
        int quantity,
        string? author,
        string? publisher,
        string? edition,
        int? publishingYear,
        string? isbn,
        decimal? vendorPrice,
        string? internalNote,
        string? vendorNote)
    {
        return new PurchaseItem(
            title,
            quantity,
            author,
            publisher,
            edition,
            publishingYear,
            isbn,
            vendorPrice,
            internalNote,
            vendorNote);
    }
}