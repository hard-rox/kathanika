using Kathanika.Core.Domain.Aggregates.AuthorAggregate;
using Kathanika.Core.Domain.Aggregates.PublisherAggregate;
using Kathanika.Core.Domain.DomainEvents;
using Kathanika.Core.Domain.Primitives;

namespace Kathanika.Core.Domain.Aggregates.PublicationAggregate;

public sealed class Publication : AggregateRoot
{
    private List<PublicationAuthor> _authors = [];
    private List<PurchaseRecord> _purchaseRecords = [];
    private List<DonationRecord> _donationRecords = [];

    public string Title { get; private set; }
    public string? Isbn { get; private set; }
    public PublicationType PublicationType { get; private set; }
    public PublicationPublisher? Publisher { get; private set; }
    public DateOnly PublishedDate { get; private set; }
    public string Edition { get; private set; }
    public string? Description { get; private set; }
    public string Language { get; private set; }
    public int CopiesAvailable { get; private set; }
    public string CallNumber { get; private set; }
    public string CoverImageFileId { get; private set; }

    public IReadOnlyList<PublicationAuthor> Authors
    {
        get { return _authors; }
        private init { _authors = value?.ToList() ?? []; }
    }

    public IReadOnlyList<PurchaseRecord> PurchaseRecords
    {
        get { return _purchaseRecords ?? []; }
        private init { _purchaseRecords = value?.ToList() ?? []; }
    }

    public IReadOnlyList<DonationRecord> DonationRecords
    {
        get { return _donationRecords ?? []; }
        private init { _donationRecords = value?.ToList() ?? []; }
    }

    private Publication(
        string title,
        string? isbn,
        PublicationType publicationType,
        DateOnly publishedDate,
        string edition,
        string callNumber,
        string coverImageFileId,
        string? description,
        string language,
        Publisher? publisher,
        IEnumerable<Author>? authors = null)
    {
        Title = title;
        Isbn = isbn;
        PublicationType = publicationType;
        PublishedDate = publishedDate;
        Edition = edition;
        CallNumber = callNumber;
        Description = description;
        Language = language;
        CoverImageFileId = coverImageFileId;

        if (publisher is not null)
        {
            Publisher = new PublicationPublisher(publisher.Id, publisher.Name);
        }

        if (authors is not null)
        {
            foreach (Author author in authors)
            {
                _authors.Add(new PublicationAuthor(author.Id,
                    author.FirstName,
                    author.LastName,
                    author.DpFileId));
            }
        }
    }

    public static Result<Publication> Create(
        string title,
        string? isbn,
        PublicationType publicationType,
        DateOnly publishedDate,
        string edition,
        string callNumber,
        string coverImageFileId,
        string? description,
        string language,
        AcquisitionMethod acquisitionMethod,
        int quantity,
        Publisher? publisher,
        decimal? unitPrice,
        string? vendor,
        string? patron,
        IEnumerable<Author>? authors = null)
    {
        List<KnError> errors = [];
        if (acquisitionMethod == AcquisitionMethod.Purchase && unitPrice is null)
        {
            errors.Add(PublicationAggregateErrors.UnitPriceNull);
        }
        if (acquisitionMethod == AcquisitionMethod.Purchase && vendor is null)
        {
            errors.Add(PublicationAggregateErrors.UnitPriceNull);
        }
        if (acquisitionMethod == AcquisitionMethod.Donation && patron is null)
        {
            errors.Add(PublicationAggregateErrors.PatronNull);
        }

        if (errors.Count > 0)
            return Result.Failure<Publication>(errors);

        Publication publication = new(
            title,
            isbn,
            publicationType,
            publishedDate,
            edition,
            callNumber,
            coverImageFileId,
            description,
            language,
            publisher,
            authors);
        if (acquisitionMethod == AcquisitionMethod.Purchase)
            publication.RecordPurchase(unitPrice!.Value, quantity, vendor!);

        else if (acquisitionMethod == AcquisitionMethod.Donation)
            publication.RecordDonation(quantity, patron!);

        publication.AddDomainEvent(new FileUsedDomainEvent(coverImageFileId));

        return Result.Success(publication);
    }

    public Result Update(
        string? title,
        string? isbn,
        PublicationType? publicationType,
        Publisher? publisher,
        DateOnly? publishedDate,
        string? edition,
        string? callNumber,
        string? coverImageFileId,
        string? description,
        string? language)
    {
        Title = !string.IsNullOrWhiteSpace(title?.Trim()) ? title.Trim() : Title;
        Isbn = !string.IsNullOrWhiteSpace(isbn?.Trim()) ? isbn.Trim() : Isbn;
        PublicationType = publicationType is not null ? publicationType.Value : PublicationType;
        PublishedDate = publishedDate is not null ? publishedDate.Value : PublishedDate;
        Edition = !string.IsNullOrWhiteSpace(edition?.Trim()) ? edition.Trim() : Edition;
        CallNumber = !string.IsNullOrWhiteSpace(callNumber?.Trim()) ? callNumber.Trim() : CallNumber;
        Description = !string.IsNullOrWhiteSpace(description?.Trim()) ? description.Trim() : Description;
        Language = !string.IsNullOrWhiteSpace(language?.Trim()) ? language.Trim() : Language;

        Publisher = publisher is not null ? new PublicationPublisher(publisher.Id, publisher.Name) : null;

        if (!string.IsNullOrWhiteSpace(coverImageFileId?.Trim()))
        {
            AddDomainEvent(new FileReplacedDomainEvent(CoverImageFileId, coverImageFileId));
            CoverImageFileId = coverImageFileId;
        }

        return Result.Success();
    }

    public Result UpdateAuthors(Author[] authors)
    {
        _authors.Clear();

        foreach (Author author in authors)
        {
            _authors.Add(new PublicationAuthor(author.Id,
                    author.FirstName,
                    author.LastName,
                    author.DpFileId));
        }

        return Result.Success();
    }

    public Result UpdateAuthorInfo(Author author)
    {
        PublicationAuthor? publicationAuthor = _authors.Find(x => x.Id == author.Id);
        if (publicationAuthor is null) return PublicationAggregateErrors.AuthorNotFound(author.Id);
        _authors.Remove(publicationAuthor);
        _authors.Add(new PublicationAuthor(author.Id,
                    author.FirstName,
                    author.LastName,
                    author.DpFileId));
        return Result.Success();
    }

    public Result RecordPurchase(
        decimal unitPrice,
        int quantity,
        string vendor)
    {
        _purchaseRecords ??= [];
        _purchaseRecords.Add(new PurchaseRecord(quantity, unitPrice, vendor));
        CopiesAvailable += quantity;

        return Result.Success();
    }

    public Result RecordDonation(
        int quantity,
        string patron)
    {
        _donationRecords ??= [];
        _donationRecords.Add(new DonationRecord(quantity, patron));
        CopiesAvailable += quantity;

        return Result.Success();
    }
}
