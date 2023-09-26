namespace Kathanika.Application.Features.Publishers.Commands;

internal sealed class AddPublisherCommandHandler
    : IRequestHandler<AddPublisherCommand,Publisher>
{
    private readonly IPublisherRepository publisherRepository;

    public AddPublisherCommandHandler(IPublisherRepository publisherRepository)
    {
        this.publisherRepository = publisherRepository;
    }

    public async Task<Publisher> Handle(AddPublisherCommand request, CancellationToken cancellationToken)
    {
        var publisher = Publisher.Create(
            request.Name,
            request.Description,
            request.ContactInformation
            );

        var addPublisher = await publisherRepository.AddAsync(publisher);
        return addPublisher;
    }
}
