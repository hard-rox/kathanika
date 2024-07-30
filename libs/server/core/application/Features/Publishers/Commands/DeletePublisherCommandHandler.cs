namespace Kathanika.Core.Application.Features.Publishers.Commands;

internal sealed class DeletePublisherCommandHandler(IPublisherRepository publisherRepository) : IRequestHandler<DeletePublisherCommand, Result>
{
    public async Task<Result> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
    {
        Publisher? publisher = await publisherRepository.GetByIdAsync(request.Id, cancellationToken);
        if (publisher is null)
            return Result.Failure(PublisherAggregateErrors.NotFound(request.Id));

        //TODO: var hasPublication = (await publicationRepository.ListAllAsync(x => x.Publisher))
        await publisherRepository.DeleteAsync(request.Id, cancellationToken);
        return Result.Success();
    }
}
