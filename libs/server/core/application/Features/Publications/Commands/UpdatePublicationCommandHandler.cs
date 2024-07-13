using Kathanika.Core.Domain.Exceptions;

namespace Kathanika.Core.Application.Features.Publications.Commands;

internal sealed class UpdatePublicationCommandHandler(
    IPublicationRepository publicationRepository,
    IAuthorRepository authorRepository,
    IPublisherRepository publisherRepository
) : IRequestHandler<UpdatePublicationCommand, Publication>
{
    public async Task<Publication> Handle(UpdatePublicationCommand request, CancellationToken cancellationToken)
    {
        Publication publication = await publicationRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundWithTheIdException(typeof(Publication), request.Id);

        IReadOnlyList<Author>? authors = request.Patch.AuthorIds is not null ?
            await authorRepository.ListAllAsync(x => request.Patch.AuthorIds.Contains(x.Id), cancellationToken)
            : null;
        Publisher? publisher = request.Patch.PublisherId is null ? null : await publisherRepository.GetByIdAsync(request.Patch.PublisherId, cancellationToken);

        publication.Update(
            request.Patch.Title,
            request.Patch.Isbn,
            request.Patch.PublicationType,
            publisher,
            request.Patch.PublishedDate,
            request.Patch.Edition,
            request.Patch.CallNumber,
            request.Patch.CoverImageFileId,
            request.Patch.Description,
            request.Patch.Language
        );

        if (authors is not null)
        {
            publication.UpdateAuthors([.. authors]);
        }

        await publicationRepository.UpdateAsync(publication, cancellationToken);

        return publication;
    }
}
