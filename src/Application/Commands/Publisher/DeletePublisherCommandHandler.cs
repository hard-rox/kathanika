using Kathanika.Domain.Exceptions;

namespace Kathanika.Application.Commands;

internal sealed class DeletePublisherCommandHandler : IRequestHandler<DeletePublisherCommand>
{
    private readonly IPublisherRepository publisherRepository;
    private readonly IPublicationRepository publicationRepository;

    public DeletePublisherCommandHandler(IPublisherRepository publisherRepository, IPublicationRepository publicationRepository)
    {
        this.publisherRepository = publisherRepository;
        this.publicationRepository = publicationRepository;
    }

    public async Task Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
    {
        _ = await publisherRepository.GetByIdAsync(request.Id) ??
        throw new NotFoundWithTheIdException(typeof(Publisher),request.Id);

        //var hasPublication = (await publicationRepository.ListAllAsync(x => x.Publisher)) todo
        await publisherRepository.DeleteAsync(request.Id);
    }
}
