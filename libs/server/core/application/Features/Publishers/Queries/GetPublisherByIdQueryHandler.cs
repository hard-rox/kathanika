namespace Kathanika.Core.Application.Features.Publishers.Queries;

internal sealed class GetPublisherByIdQueryHandler(IPublisherRepository publisherRepository)
    : IRequestHandler<GetPublisherByIdQuery, Result<Publisher>>
{
    public async Task<Result<Publisher>> Handle(GetPublisherByIdQuery request, CancellationToken cancellationToken)
    {
        Publisher? publisher = await publisherRepository.GetByIdAsync(request.Id, cancellationToken);
        if (publisher is null)
            return Result.Failure<Publisher>(PublisherAggregateErrors.NotFound(request.Id));

        return Result.Success(publisher);
    }
}
