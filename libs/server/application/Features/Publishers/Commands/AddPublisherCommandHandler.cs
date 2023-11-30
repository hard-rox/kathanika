namespace Kathanika.Application.Features.Publishers.Commands;

internal sealed class AddPublisherCommandHandler(IPublisherRepository publisherRepository)
        : IRequestHandler<AddPublisherCommand, Publisher>
{
    public async Task<Publisher> Handle(AddPublisherCommand request, CancellationToken cancellationToken)
    {
        Publisher publisher = Publisher.Create(
            request.Name,
            request.Description,
            request.ContactInformation
            );

        Publisher addPublisher = await publisherRepository.AddAsync(publisher, cancellationToken);
        return addPublisher;
    }
}
