namespace Kathanika.Core.Application.Features.Publications.Queries;

internal sealed class GetPublicationByIdQueryHandler(IPublicationRepository publicationRepository)
        : IRequestHandler<GetPublicationByIdQuery, Publication?>
{
    public async Task<Publication?> Handle(GetPublicationByIdQuery request, CancellationToken cancellationToken)
    {
        Publication? publication = await publicationRepository.GetByIdAsync(request.Id, cancellationToken);
        return publication;
    }
}
