namespace Kathanika.Application.Features.Publishers.Queries;

internal sealed class GetPublisherByIdQueryHandler : IRequestHandler<GetPublisherByIdQuery, Publisher?>
{
    private readonly IPublisherRepository publisherRepository;

    public GetPublisherByIdQueryHandler(IPublisherRepository publisherRepository)
    {
        this.publisherRepository = publisherRepository;
    }

    public async Task<Publisher?> Handle(GetPublisherByIdQuery request, CancellationToken cancellationToken)
    {
        Publisher? publisher = await publisherRepository.GetByIdAsync(request.Id, cancellationToken);
        return publisher;
    }
}
