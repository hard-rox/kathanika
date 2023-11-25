namespace Kathanika.Application.Features.Publications.Queries;

internal sealed class GetPublicationByIdQueryHandler
    : IRequestHandler<GetPublicationByIdQuery, Publication?>
{
    private readonly IPublicationRepository publicationRepository;

    public GetPublicationByIdQueryHandler(IPublicationRepository publicationRepository)
    {
        this.publicationRepository = publicationRepository;
    }

    public async Task<Publication?> Handle(GetPublicationByIdQuery request, CancellationToken cancellationToken)
    {
        Publication? publication = await publicationRepository.GetByIdAsync(request.Id, cancellationToken);
        return publication;
    }
}
