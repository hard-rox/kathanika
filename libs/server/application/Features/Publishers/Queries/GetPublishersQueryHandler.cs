namespace Kathanika.Application.Features.Publishers.Queries;

internal sealed class GetPublishersQueryHandler(IPublisherRepository publisherRepository) : IRequestHandler<GetPublishersQuery, IQueryable<Publisher>>
{
    public async Task<IQueryable<Publisher>> Handle(GetPublishersQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Publisher> publisherQuery = await Task.Run(() => publisherRepository.AsQueryable());
        return publisherQuery;
    }
}
