using Kathanika.Domain.DomainEvents;

namespace Kathanika.Application.Publications.Events;

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
        Author? author = await _authorRepository.GetByIdAsync(notification.AuthorId);
        if (author is null) return;

        IReadOnlyList<Publication> publications = await _publicationRepository
            .ListAllByAuthorIdAsync(notification.AuthorId);
        
        foreach (Publication publication in publications)
        {
            if (publication is null) continue;

            publication.AddOrUpdateAuthors(author);

            await _publicationRepository.UpdateAsync(publication);
        }
    }
}