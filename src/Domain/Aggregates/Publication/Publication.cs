using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates;

public sealed class Publication : AggregateRoot
{
    private List<PublicationAuthor>? _authors = null;

    public string Title { get; private set; } = string.Empty;
    public string? Isbn { get; private set; } = string.Empty;
    public PublicationType PublicationType { get; private set; }
    public IReadOnlyList<PublicationAuthor>? Authors
    {
        get
        {
            return _authors;
        }
        init
        {
            _authors = value?.ToList() ?? null;
        }
    }
    public string Publisher { get; private set; } = string.Empty;
    public DateTime PublishedDate { get; private set; }
    public string Edition { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Language { get; private set; } = string.Empty;
    public decimal BuyingPrice { get; private set; }
    public int CopiesAvailable { get; private set; }

    private Publication(
        string title,
        string? isbn,
        PublicationType publicationType,
        string publisher,
        DateTime publishedDate,
        decimal buyingPrice,
        int copiesAvailable)
    {
        Title = title;
        Isbn = isbn;
        PublicationType = publicationType;
        Publisher = publisher;
        PublishedDate = publishedDate.Date;
        BuyingPrice = buyingPrice;
        CopiesAvailable = copiesAvailable;
    }

    public static Publication Create(
        string title,
        string? isbn,
        PublicationType publicationType,
        string publisher,
        DateTime publishedDate,
        decimal buyingPrice,
        int copiesAvailable,
        IEnumerable<Author>? authors = null)
    {
        var publication = new Publication(
            title,
            isbn,
            publicationType,
            publisher,
            publishedDate,
            buyingPrice,
            copiesAvailable);

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
}
