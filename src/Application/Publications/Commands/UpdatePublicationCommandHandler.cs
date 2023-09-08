using Kathanika.Domain.Exceptions;

namespace Kathanika.Application.Publications.Commands;

internal sealed class UpdatePublicationCommandHandler : IRequestHandler<UpdatePublicationCommand, Publication>
{
    private readonly IPublicationRepository _publicationRepository;
    private readonly IAuthorRepository _authorRepository;

    public UpdatePublicationCommandHandler(IPublicationRepository publicationRepository, IAuthorRepository authorRepository)
    {
        _publicationRepository = publicationRepository;
        _authorRepository = authorRepository;
    }

    public async Task<Publication> Handle(UpdatePublicationCommand request, CancellationToken cancellationToken)
    {
        var publication = await _publicationRepository.GetByIdAsync(request.Id)
            ?? throw new NotFoundWithTheIdException(typeof(Publication), request.Id);

        var authors = request.Patch.AuthorIds is not null ?
            await _authorRepository.ListAllAsync(x => request.Patch.AuthorIds.Contains(x.Id))
            : null;

        publication.Update(
            request.Patch.Title,
            request.Patch.Isbn,
            request.Patch.PublicationType,
            request.Patch.Publisher,
            request.Patch.PublishedDate,
            request.Patch.BuyingPrice,
            request.Patch.CopiesAvailable,
            request.Patch.CallNumber
        );

        if (authors is not null && authors.Count > 0)
        {
            publication.AddOrUpdateAuthors(authors.ToArray());
        }

        await _publicationRepository.UpdateAsync(publication);

        return publication;
    }
}