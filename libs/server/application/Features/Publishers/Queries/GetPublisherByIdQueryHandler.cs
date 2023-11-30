namespace Kathanika.Application.Features.Publishers.Queries;

internal sealed class GetPublisherByIdQueryHandler(IPublisherRepository publisherRepository) : IRequestHandler<GetPublisherByIdQuery, Publisher?>
{
    public async Task<Publisher?> Handle(GetPublisherByIdQuery request, CancellationToken cancellationToken)
    {
        Publisher? publisher = await publisherRepository.GetByIdAsync(request.Id, cancellationToken);
        return publisher;
    }
}
