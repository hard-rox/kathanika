namespace Kathanika.Application.Features.Publications.Commands;

internal sealed class AcquirePublicationCommandHandler(IPublicationRepository publicationRepository, IAuthorRepository authorRepository)
        : IRequestHandler<AcquirePublicationCommand, Publication>
{
    public async Task<Publication> Handle(AcquirePublicationCommand request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Author> authors = await authorRepository.ListAllAsync(x => request.AuthorIds.Contains(x.Id), cancellationToken);

        Publication publication = Publication.Create(
            request.Title,
            request.Isbn,
            request.PublicationType,
            request.Publisher,
            request.PublishedDate,
            request.Edition,
            request.CallNumber,
            request.Description,
            request.Language,
            request.AcquisitionMethod,
            request.Quantity,
            request.UnitPrice,
            request.Vendor,
            request.Patron,
            authors);

        Publication addedPublication = await publicationRepository.AddAsync(publication, cancellationToken);
        return addedPublication;
    }
}
