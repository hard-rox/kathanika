namespace Kathanika.Application.Commands;

internal sealed class UpdatePublisherCommandHandle : IRequestHandler<UpdatePublisherCommand, Publisher>
{
    private readonly IPublisherRepository publisherRepository;

    public UpdatePublisherCommandHandle(IPublisherRepository publisherRepository)
    {
        this.publisherRepository = publisherRepository;
    }

    public async Task<Publisher> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisher = await publisherRepository.GetByIdAsync(request.Id);

        publisher.Update(
            request.Patch.PublisherName,
            request.Patch.Description,
            request.Patch.ContactInformation
            );
        await publisherRepository.UpdateAsync(publisher);
        return publisher;
    }
}
