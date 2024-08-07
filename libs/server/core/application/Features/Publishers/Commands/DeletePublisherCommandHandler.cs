namespace Kathanika.Core.Application.Features.Publishers.Commands;

internal sealed class DeletePublisherCommandHandler(
    IPublisherRepository publisherRepository,
    IPublicationRepository publicationRepository
) : IRequestHandler<DeletePublisherCommand, Result>
{
    public async Task<Result> Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
    {
        Publisher? publisher = await publisherRepository.GetByIdAsync(request.Id, cancellationToken);
        if (publisher is null)
            return PublisherAggregateErrors.NotFound(request.Id);

        bool hasPublication = await publicationRepository
            .ExistsAsync(x => x.Publisher != null && x.Publisher.Id == request.Id, cancellationToken);

        if (hasPublication)
            return PublisherAggregateErrors.HasPublication;
        await publisherRepository.DeleteAsync(request.Id, cancellationToken);
        return Result.Success();
    }
}
