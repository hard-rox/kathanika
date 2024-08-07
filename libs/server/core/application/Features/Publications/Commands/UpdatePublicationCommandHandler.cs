namespace Kathanika.Core.Application.Features.Publications.Commands;

internal sealed class UpdatePublicationCommandHandler(
    IPublicationRepository publicationRepository,
    IAuthorRepository authorRepository,
    IPublisherRepository publisherRepository
) : IRequestHandler<UpdatePublicationCommand, Result<Publication>>
{
    public async Task<Result<Publication>> Handle(UpdatePublicationCommand request, CancellationToken cancellationToken)
    {
        Publication? publication = await publicationRepository.GetByIdAsync(request.Id, cancellationToken);
        if (publication is null)
            return Result.Failure<Publication>(PublicationAggregateErrors.NotFound(request.Id));

        IReadOnlyList<Author>? authors = request.Patch.AuthorIds is not null ?
            await authorRepository.ListAllAsync(x => request.Patch.AuthorIds.Contains(x.Id), cancellationToken)
            : null;
        Publisher? publisher = request.Patch.PublisherId is null ? null : await publisherRepository.GetByIdAsync(request.Patch.PublisherId, cancellationToken);

        Result publicationUpdateResult = publication.Update(
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

        if (publicationUpdateResult.IsFailure)
            return Result.Failure<Publication>(publicationUpdateResult.Errors);

        if (authors is not null)
        {
            Result updateAuthorResult = publication.UpdateAuthors([.. authors]);

            if (updateAuthorResult.IsFailure)
                return Result.Failure<Publication>(updateAuthorResult.Errors);
        }

        await publicationRepository.UpdateAsync(publication, cancellationToken);

        return Result.Success(publication);
    }
}
