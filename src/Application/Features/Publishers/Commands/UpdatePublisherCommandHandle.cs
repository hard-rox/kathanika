using Kathanika.Domain.Exceptions;

namespace Kathanika.Application.Features.Publishers.Commands;

internal sealed class UpdatePublisherCommandHandle : IRequestHandler<UpdatePublisherCommand, Publisher>
{
    private readonly IPublisherRepository publisherRepository;

    public UpdatePublisherCommandHandle(IPublisherRepository publisherRepository)
    {
        this.publisherRepository = publisherRepository;
    }

    public async Task<Publisher> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisher = await publisherRepository.GetByIdAsync(request.Id) ??
            throw new NotFoundWithTheIdException(typeof(Publisher), request.Id);

        publisher.Update(
            request.Patch.Name,
            request.Patch.Description,
            request.Patch.ContactInformation
            );
        await publisherRepository.UpdateAsync(publisher);
        return publisher;
    }
}
