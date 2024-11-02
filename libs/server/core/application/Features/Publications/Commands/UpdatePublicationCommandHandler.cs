namespace Kathanika.Core.Application.Features.Publications.Commands;

internal sealed class UpdatePublicationCommandHandler(
    IPublicationRepository publicationRepository
) : IRequestHandler<UpdatePublicationCommand, Result<Publication>>
{
    public async Task<Result<Publication>> Handle(UpdatePublicationCommand request, CancellationToken cancellationToken)
    {
        Publication? publication = await publicationRepository.GetByIdAsync(request.Id, cancellationToken);
        if (publication is null)
            return Result.Failure<Publication>(PublicationAggregateErrors.NotFound(request.Id));

        Result publicationUpdateResult = publication.Update(
            request.Patch.Title,
            request.Patch.Isbn,
            request.Patch.PublicationType,
            request.Patch.PublishedDate,
            request.Patch.Edition,
            request.Patch.CallNumber,
            request.Patch.CoverImageFileId,
            request.Patch.Description,
            request.Patch.Language
        );

        if (publicationUpdateResult.IsFailure)
            return Result.Failure<Publication>(publicationUpdateResult.Errors);

        await publicationRepository.UpdateAsync(publication, cancellationToken);

        return Result.Success(publication);
    }
}
