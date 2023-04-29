using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates;

public sealed class Publication : AggregateRoot
{
    private List<PublicationAuthor> _authors = new();

    public string Title { get; private set; } = string.Empty;
    public string? Isbn { get; private set; } = string.Empty;
    public PublicationType PublicationType { get; private set; }
    public string Publisher { get; private set; } = string.Empty;
    public DateTime PublishedDate { get; private set; }
    public string Edition { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Language { get; private set; } = string.Empty;
    public decimal BuyingPrice { get; private set; }
    public int CopiesAvailable { get; private set; }
    public string CallNumber { get; private set; }

    public IReadOnlyList<PublicationAuthor> Authors
    {
        get
        {
            return _authors;
        }
        init
        {
            _authors = value?.ToList() ?? new();
        }
    }

    private Publication(
        string title,
        string? isbn,
        PublicationType publicationType,
        string publisher,
        DateTime publishedDate,
        decimal buyingPrice,
        int copiesAvailable,
        string callNumber)
    {
        Title = title;
        Isbn = isbn;
        PublicationType = publicationType;
        Publisher = publisher;
        PublishedDate = publishedDate.Date;
        BuyingPrice = buyingPrice;
        CopiesAvailable = copiesAvailable;
        CallNumber = callNumber;
    }

    public static Publication Create(
        string title,
        string? isbn,
        PublicationType publicationType,
        string publisher,
        DateTime publishedDate,
        decimal buyingPrice,
        int copiesAvailable,
        string callNumber,
        IEnumerable<Author>? authors = null)
    {
        var publication = new Publication(
            title,
            isbn,
            publicationType,
            publisher,
            publishedDate,
            buyingPrice,
            copiesAvailable,
            callNumber);

        if (authors is not null)
        {
            publication._authors = new();
            foreach (var author in authors)
            {
                publication._authors.Add(new PublicationAuthor(author.Id,
                    author.FirstName,
                    author.LastName));
            }
        }

        return publication;
    }

    public void Update(
        string title,
        string isbn,
        PublicationType publicationType,
        string publisher,
        DateTime? publishedDate,
        decimal? buyingPrice,
        int? copiesAvailable,
        string callNumber,
        IEnumerable<Author>? authors)
    {
        Title = !string.IsNullOrEmpty(title) ? title : Title;
        Isbn = !string.IsNullOrEmpty(isbn) ? isbn : Isbn;
        PublicationType = publicationType;
        Publisher = !string.IsNullOrEmpty(publisher) ? publisher : Publisher;
        PublishedDate = publishedDate is not null ? (DateTime)publishedDate : PublishedDate;
        BuyingPrice = buyingPrice is not null? (decimal)buyingPrice : BuyingPrice;
        CopiesAvailable = copiesAvailable is not null? (int)copiesAvailable : CopiesAvailable;
        CallNumber = !string.IsNullOrEmpty(callNumber) ? callNumber : CallNumber;

        if (authors is not null && authors.Count() > 0)
        {
            _authors = new();
            foreach (var author in authors)
            {
                _authors.Add(new PublicationAuthor(author.Id,
                    author.FirstName,
                    author.LastName));
            }
        }
        else _authors = new();
    }
}
