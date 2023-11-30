using Kathanika.Domain.DomainEvents;

namespace Kathanika.Application.Features.Publications.Events;

internal sealed class AuthorUpdatedDomainEventHandler(ILogger<AuthorUpdatedDomainEventHandler> logger, IAuthorRepository authorRepository, IPublicationRepository publicationRepository) : INotificationHandler<AuthorUpdatedDomainEvent>
{
    public async Task Handle(AuthorUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        //TODO: Logging and optimize performance in loop.
        Author? author = await authorRepository.GetByIdAsync(notification.AuthorId, cancellationToken);

        if (author is null) return;

        IReadOnlyList<Publication> publications = await publicationRepository
            .ListAllByAuthorIdAsync(notification.AuthorId, cancellationToken);

        foreach (Publication publication in publications)
        {
            if (publication is null) continue;

            publication.UpdateAuthorInfo(author);

            await publicationRepository.UpdateAsync(publication, cancellationToken);
        }
    }
}
