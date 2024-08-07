namespace Kathanika.Core.Application.Features.Publications.Commands;

internal sealed class AcquirePublicationCommandHandler(
    IPublicationRepository publicationRepository,
    IAuthorRepository authorRepository,
    IPublisherRepository publisherRepository)
        : IRequestHandler<AcquirePublicationCommand, Result<Publication>>
{
    public async Task<Result<Publication>> Handle(AcquirePublicationCommand request, CancellationToken cancellationToken)
    {
        IReadOnlyList<Author> authors = await authorRepository.ListAllAsync(x => request.AuthorIds.Contains(x.Id), cancellationToken);
        Publisher? publisher = await publisherRepository.GetByIdAsync(request.PublisherId, cancellationToken);

        Result<Publication> publicationCreateResult = Publication.Create(
            request.Title,
            request.Isbn,
            request.PublicationType,
            request.PublishedDate,
            request.Edition,
            request.CallNumber,
            request.CoverImageFileId,
            request.Description,
            request.Language,
            request.AcquisitionMethod,
            request.Quantity,
            publisher,
            request.UnitPrice,
            request.Vendor,
            request.Patron,
            authors);

        if (publicationCreateResult.IsFailure)
            return publicationCreateResult;

        Publication addedPublication = await publicationRepository.AddAsync(publicationCreateResult.Value, cancellationToken);
        return Result.Success(addedPublication);
    }
}
