namespace Kathanika.Application.Queries;

internal sealed class GetPublisherByIdQueryHandler : IRequestHandler<GetPublisherByIdQuery, Publisher>
{
    private readonly IPublisherRepository publisherRepository;

    public GetPublisherByIdQueryHandler(IPublisherRepository publisherRepository)
    {
        this.publisherRepository = publisherRepository;
    }

    public async Task<Publisher> Handle(GetPublisherByIdQuery request, CancellationToken cancellationToken)
    {
        var publisher = await publisherRepository.GetByIdAsync(request.Id);
        return publisher;
    }
}
