using Kathanika.Domain.Exceptions;

namespace Kathanika.Application.Features.Publications.Commands;

internal sealed class UpdatePublicationCommandHandler(IPublicationRepository publicationRepository, IAuthorRepository authorRepository) : IRequestHandler<UpdatePublicationCommand, Publication>
{
    public async Task<Publication> Handle(UpdatePublicationCommand request, CancellationToken cancellationToken)
    {
        Publication publication = await publicationRepository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new NotFoundWithTheIdException(typeof(Publication), request.Id);

        IReadOnlyList<Author>? authors = request.Patch.AuthorIds is not null ?
            await authorRepository.ListAllAsync(x => request.Patch.AuthorIds.Contains(x.Id), cancellationToken)
            : null;

        publication.Update(
            request.Patch.Title,
            request.Patch.Isbn,
            request.Patch.PublicationType,
            request.Patch.Publisher,
            request.Patch.PublishedDate,
            request.Patch.Edition,
            request.Patch.CallNumber,
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
