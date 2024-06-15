namespace Kathanika.Core.Application.Features.Publications.Queries;

internal sealed class GetPublicationsQueryHandler(IPublicationRepository publicationRepository)
        : IRequestHandler<GetPublicationsQuery, IQueryable<Publication>>
{
    public async Task<IQueryable<Publication>> Handle(GetPublicationsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Publication> publicationQuery = await Task.Run(() => publicationRepository.AsQueryable(), cancellationToken);
        return publicationQuery;
    }
}
