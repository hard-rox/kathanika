namespace Kathanika.Application.Queries;

internal sealed class GetPublishersQueryHandler : IRequestHandler<GetPublishersQuery, IQueryable<Publisher>>
{
    private readonly IPublisherRepository publisherRepository;

    public GetPublishersQueryHandler(IPublisherRepository publisherRepository)
    {
        this.publisherRepository = publisherRepository;
    }

    public async Task<IQueryable<Publisher>> Handle(GetPublishersQuery request, CancellationToken cancellationToken)
    {
        var publisherQuery = await Task.Run(() => publisherRepository.AsQueryable());
        return publisherQuery;
    }
}
