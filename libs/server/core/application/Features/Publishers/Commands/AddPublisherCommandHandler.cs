namespace Kathanika.Core.Application.Features.Publishers.Commands;

internal sealed class AddPublisherCommandHandler(IPublisherRepository publisherRepository)
        : IRequestHandler<AddPublisherCommand, Result<Publisher>>
{
    public async Task<Result<Publisher>> Handle(AddPublisherCommand request, CancellationToken cancellationToken)
    {
        Result<Publisher> publisherCreateResult = Publisher.Create(
            request.Name,
            request.Description,
            request.ContactInformation
        );

        if (publisherCreateResult.IsFailure)
            return publisherCreateResult;

        Publisher addPublisher = await publisherRepository.AddAsync(publisherCreateResult.Value, cancellationToken);
        return Result.Success(addPublisher);
    }
}
