namespace Kathanika.Core.Application.Features.Publishers.Commands;

internal sealed class UpdatePublisherCommandHandler(IPublisherRepository publisherRepository)
    : IRequestHandler<UpdatePublisherCommand, Result<Publisher>>
{
    public async Task<Result<Publisher>> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
    {
        Publisher? publisher = await publisherRepository.GetByIdAsync(request.Id, cancellationToken);
        if (publisher is null)
            return Result.Failure<Publisher>(PublisherAggregateErrors.NotFound(request.Id));

        Result publisherUpdateResult = publisher.Update(
            request.Patch.Name,
            request.Patch.Description,
            request.Patch.ContactInformation
            );
        if (publisherUpdateResult.IsFailure)
            return Result.Failure<Publisher>(publisherUpdateResult.Errors);

        await publisherRepository.UpdateAsync(publisher, cancellationToken);
        return Result.Success(publisher);
    }
}
