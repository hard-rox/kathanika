namespace Kathanika.Application.Features.Publications.Commands;

internal sealed class AddPublicationCommandHandler
    : IRequestHandler<AddPublicationCommand, Publication>
{
    private readonly IPublicationRepository publicationRepository;
    private readonly IAuthorRepository authorRepository;

    public AddPublicationCommandHandler(IPublicationRepository publicationRepository, IAuthorRepository authorRepository)
    {
        this.publicationRepository = publicationRepository;
        this.authorRepository = authorRepository;
    }

    public async Task<Publication> Handle(AddPublicationCommand request, CancellationToken cancellationToken)
    {
        var authors = await authorRepository.ListAllAsync(x => request.AuthorIds.Contains(x.Id));

        Publication publication = Publication.Create(
            request.Title,
            request.Isbn,
            request.PublicationType,
            request.Publisher,
            request.PublishedDate,
            request.BuyingPrice,
            request.CopiesPurchased,
            request.CallNumber,
            authors);

        var addedPublication = await publicationRepository.AddAsync(publication);
        return addedPublication;
    }
}
