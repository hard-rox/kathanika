using Kathanika.Domain.Primitives;

namespace Kathanika.Domain.Aggregates;

public sealed class Publication : AggregateRoot
{
    private List<PublicationAuthor> _authors = [];
    private List<PurchaseRecord> _purchaseRecords = [];

    public string Title { get; private set; }
    public string? Isbn { get; private set; }
    public PublicationType PublicationType { get; private set; }
    public string Publisher { get; private set; }
    public DateOnly PublishedDate { get; private set; }
    public string Edition { get; private set; }
    public string Description { get; private set; }
    public string Language { get; private set; }
    public int CopiesAvailable { get; private set; }
    public string CallNumber { get; private set; }

    public IReadOnlyList<PublicationAuthor> Authors
    {
        get
        {
            return _authors;
        }
        private init
        {
            _authors = value?.ToList() ?? [];
        }
    }

    public IReadOnlyList<PurchaseRecord> PurchaseRecords
    {
        get
        {
            return _purchaseRecords;
        }
        private init
        {
            _purchaseRecords = value?.ToList() ?? [];
        }
    }

    private Publication(
        string title,
        string? isbn,
        PublicationType publicationType,
        string publisher,
        DateOnly publishedDate,
        string edition,
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

    public static Publication Create(
        string title,
        string? isbn,
        PublicationType publicationType,
        string publisher,
        DateOnly publishedDate,
        string edition,
        string callNumber,
        string description,
        string language,
        decimal unitPrice,
        int quantity,
        string vendor,
    IEnumerable<Author>? authors = null
    )
    {
        Publication publication = Create(
            title,
            isbn,
            publicationType,
            publisher,
            publishedDate,
            edition,
            quantity,
            callNumber,
            description,
            language,
            authors
        );

        publication.RecordPurchase(unitPrice, quantity, vendor);
        return publication;
    }

    public void Update(
        string? title,
        string? isbn,
        PublicationType? publicationType,
        string? publisher,
        DateOnly? publishedDate,
        string? edition,
        int? copiesAvailable,
        string? callNumber)
    {
        Title = !string.IsNullOrEmpty(title) ? title : Title;
        Isbn = !string.IsNullOrEmpty(isbn) ? isbn : Isbn;
        PublicationType = publicationType is not null ? (PublicationType)publicationType : PublicationType;
        Publisher = !string.IsNullOrEmpty(publisher) ? publisher : Publisher;
        PublishedDate = publishedDate is not null ? (DateOnly)publishedDate : PublishedDate;
        Edition = edition is not null ? edition : Edition;
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
        PublicationAuthor? publicationAuthor = _authors.FirstOrDefault(x => x.Id == author.Id);
        if (publicationAuthor is null) return;
        _authors.Remove(publicationAuthor);
        _authors.Add(new PublicationAuthor(author.Id,
                    author.FirstName,
                    author.LastName));
    }

    public void RecordPurchase(
        decimal unitPrice,
        int quantity,
        string vendor)
    {
        _purchaseRecords ??= [];
        _purchaseRecords.Add(new PurchaseRecord(quantity, unitPrice, vendor));
        CopiesAvailable += quantity;
    }
}
