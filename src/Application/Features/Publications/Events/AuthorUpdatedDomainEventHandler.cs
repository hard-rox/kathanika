using Kathanika.Domain.DomainEvents;

namespace Kathanika.Application.Features.Publications.Events;

internal sealed class AuthorUpdatedDomainEventHandler : INotificationHandler<AuthorUpdatedDomainEvent>
{
    private readonly ILogger<AuthorUpdatedDomainEventHandler> _logger;
    private readonly IAuthorRepository _authorRepository;
    private readonly IPublicationRepository _publicationRepository;

    public AuthorUpdatedDomainEventHandler(ILogger<AuthorUpdatedDomainEventHandler> logger, IAuthorRepository authorRepository, IPublicationRepository publicationRepository)
    {
        _logger = logger;
        _authorRepository = authorRepository;
        _publicationRepository = publicationRepository;
    }
    public async Task Handle(AuthorUpdatedDomainEvent notification, CancellationToken cancellationToken)
    {
        //TODO: Logging and optimize performance in loop.
        Author? author = await _authorRepository.GetByIdAsync(notification.AuthorId, cancellationToken);
        
        if (author is null) return;

        IReadOnlyList<Publication> publications = await _publicationRepository
            .ListAllByAuthorIdAsync(notification.AuthorId, cancellationToken);
        
        foreach (Publication publication in publications)
        {
            if (publication is null) continue;

            publication.AddOrUpdateAuthors(author);

            await _publicationRepository.UpdateAsync(publication, cancellationToken);
        }
    }
}
