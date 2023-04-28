namespace Kathanika.Application.Queries;

internal sealed class GetPublicationsQueryHandler
    : IRequestHandler<GetPublicationsQuery, IQueryable<Publication>>
{
    private readonly IPublicationRepository publicationRepository;

    public GetPublicationsQueryHandler(IPublicationRepository publicationRepository)
    {
        this.publicationRepository = publicationRepository;
    }

    public async Task<IQueryable<Publication>> Handle(GetPublicationsQuery request, CancellationToken cancellationToken)
    {
        var publicationQuery = await Task.Run(() => publicationRepository.AsQueryable());
        return publicationQuery;
    }
}
