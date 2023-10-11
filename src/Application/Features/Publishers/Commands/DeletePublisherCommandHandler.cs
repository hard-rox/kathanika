using Kathanika.Domain.Exceptions;

namespace Kathanika.Application.Features.Publishers.Commands;

internal sealed class DeletePublisherCommandHandler : IRequestHandler<DeletePublisherCommand>
{
    private readonly IPublisherRepository publisherRepository;

    public DeletePublisherCommandHandler(IPublisherRepository publisherRepository)
    {
        this.publisherRepository = publisherRepository;
    }

    public async Task Handle(DeletePublisherCommand request, CancellationToken cancellationToken)
    {
        _ = await publisherRepository.GetByIdAsync(request.Id, cancellationToken) ??
        throw new NotFoundWithTheIdException(typeof(Publisher),request.Id);

        //TODO: var hasPublication = (await publicationRepository.ListAllAsync(x => x.Publisher))
        await publisherRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
