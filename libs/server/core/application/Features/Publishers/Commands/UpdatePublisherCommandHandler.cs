using Kathanika.Core.Domain.Exceptions;

namespace Kathanika.Core.Application.Features.Publishers.Commands;

internal sealed class UpdatePublisherCommandHandler(IPublisherRepository publisherRepository) : IRequestHandler<UpdatePublisherCommand, Publisher>
{
    public async Task<Publisher> Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
    {
        Publisher publisher = await publisherRepository.GetByIdAsync(request.Id, cancellationToken) ??
            throw new NotFoundWithTheIdException(typeof(Publisher), request.Id);

        publisher.Update(
            request.Patch.Name,
            request.Patch.Description,
            request.Patch.ContactInformation
            );
        await publisherRepository.UpdateAsync(publisher, cancellationToken);
        return publisher;
    }
}
