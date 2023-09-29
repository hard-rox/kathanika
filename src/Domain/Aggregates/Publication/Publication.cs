using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates;

public sealed class Publication : AggregateRoot
{
    private List<PublicationAuthor> _authors = new();

    public string Title { get; private set; } = string.Empty;
    public string? Isbn { get; private set; } = string.Empty;
    public PublicationType PublicationType { get; private set; }
    public string Publisher { get; private set; } = string.Empty;
    public DateOnly PublishedDate { get; private set; }
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
        DateOnly publishedDate,
        decimal buyingPrice,
        int copiesAvailable,
        string callNumber)
    {
        Title = title;
        Isbn = isbn;
        PublicationType = publicationType;
        Publisher = publisher;
        PublishedDate = publishedDate;
        BuyingPrice = buyingPrice;
        CopiesAvailable = copiesAvailable;
        CallNumber = callNumber;
    }

    public static Publication Create(
        string title,
        string? isbn,
        PublicationType publicationType,
        string publisher,
        DateOnly publishedDate,
        decimal buyingPrice,
        int copiesAvailable,
        string callNumber,
        IEnumerable<Author>? authors = null)
    {
        Publication publication = new(
            title,
            isbn,
            publicationType,
            publisher,
            publishedDate,
            buyingPrice,
            copiesAvailable,
            callNumber)
        {
            _authors = new()
        };
        
        if (authors is not null)
        {
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
        string? title,
        string? isbn,
        PublicationType? publicationType,
        string? publisher,
        DateOnly? publishedDate,
        decimal? buyingPrice,
        int? copiesAvailable,
        string? callNumber)
    {
        Title = !string.IsNullOrEmpty(title) ? title : Title;
        Isbn = !string.IsNullOrEmpty(isbn) ? isbn : Isbn;
        PublicationType = publicationType is not null ? (PublicationType)publicationType : PublicationType;
        Publisher = !string.IsNullOrEmpty(publisher) ? publisher : Publisher;
        PublishedDate = publishedDate is not null ? (DateOnly)publishedDate : PublishedDate;
        BuyingPrice = buyingPrice is not null ? (decimal)buyingPrice : BuyingPrice;
        CopiesAvailable = copiesAvailable is not null ? (int)copiesAvailable : CopiesAvailable;
        CallNumber = !string.IsNullOrEmpty(callNumber) ? callNumber : CallNumber;
    }

    public void AddOrUpdateAuthors(params Author[] authors)
    {
        foreach (Author author in authors)
        {
            _authors.RemoveAll(x => x.Id == author.Id);

            _authors.Add(new PublicationAuthor(author.Id,
                    author.FirstName,
                    author.LastName));
        }
    }
}
