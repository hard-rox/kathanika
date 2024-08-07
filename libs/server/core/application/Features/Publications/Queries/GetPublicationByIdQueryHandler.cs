namespace Kathanika.Core.Application.Features.Publications.Queries;

internal sealed class GetPublicationByIdQueryHandler(IPublicationRepository publicationRepository)
        : IRequestHandler<GetPublicationByIdQuery, Result<Publication>>
{
    public async Task<Result<Publication>> Handle(GetPublicationByIdQuery request, CancellationToken cancellationToken)
    {
        Publication? publication = await publicationRepository.GetByIdAsync(request.Id, cancellationToken);
        if (publication is null)
            return Result.Failure<Publication>(PublicationAggregateErrors.NotFound(request.Id));

        return Result.Success(publication);
    }
}
