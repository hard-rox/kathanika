using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates;

public sealed class Publication : AggregateRoot
{
    private List<PublicationAuthor> _authors = [];

    public string Title { get; private set; }
    public string? Isbn { get; private set; }
    public PublicationType PublicationType { get; private set; }
    public string Publisher { get; private set; }
    public DateOnly PublishedDate { get; private set; }
    public string Edition { get; private set; }
    public string Description { get; private set; }
    public string Language { get; private set; }
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
            _authors = value?.ToList() ?? [];
        }
    }

    private Publication(
        string title,
        string? isbn,
        PublicationType publicationType,
        string publisher,
        DateOnly publishedDate,
        string edition,
        decimal buyingPrice,
        int copiesAvailable,
        string callNumber,
        string description,
        string language)
    {
        Title = title;
        Isbn = isbn;
        PublicationType = publicationType;
        Publisher = publisher;
        PublishedDate = publishedDate;
        Edition = edition;
        BuyingPrice = buyingPrice;
        CopiesAvailable = copiesAvailable;
        CallNumber = callNumber;
        Description = description;
        Language = language;
    }

    public static Publication Create(
        string title,
        string? isbn,
        PublicationType publicationType,
        string publisher,
        DateOnly publishedDate,
        string edition,
        decimal buyingPrice,
        int copiesAvailable,
        string callNumber,
        string description,
        string language,
        IEnumerable<Author>? authors = null)
    {
        Publication publication = new(
            title,
            isbn,
            publicationType,
            publisher,
            publishedDate,
            edition,
            buyingPrice,
            copiesAvailable,
            callNumber,
            description,
            language)
        {
            _authors = []
        };

        if (authors is not null)
        {
            foreach (Author author in authors)
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
        string? edition,
        decimal? buyingPrice,
        int? copiesAvailable,
        string? callNumber)
    {
        Title = !string.IsNullOrEmpty(title) ? title : Title;
        Isbn = !string.IsNullOrEmpty(isbn) ? isbn : Isbn;
        PublicationType = publicationType is not null ? (PublicationType)publicationType : PublicationType;
        Publisher = !string.IsNullOrEmpty(publisher) ? publisher : Publisher;
        PublishedDate = publishedDate is not null ? (DateOnly)publishedDate : PublishedDate;
        Edition = edition is not null ? edition : Edition;
        BuyingPrice = buyingPrice is not null ? (decimal)buyingPrice : BuyingPrice;
        CopiesAvailable = copiesAvailable is not null ? (int)copiesAvailable : CopiesAvailable;
        CallNumber = !string.IsNullOrEmpty(callNumber) ? callNumber : CallNumber;
    }

    public void UpdateAuthors(Author[] authors)
    {
        _authors.Clear();

        foreach (Author author in authors)
        {
            _authors.Add(new PublicationAuthor(author.Id,
                    author.FirstName,
                    author.LastName));
        }
    }

    public void UpdateAuthorInfo(Author author)
    {
        PublicationAuthor? publicationAuthor = _authors.Find(x => x.Id == author.Id);
        if (publicationAuthor is null) return;
        _authors.Remove(publicationAuthor);
        _authors.Add(new PublicationAuthor(author.Id,
                    author.FirstName,
                    author.LastName));
    }
}
