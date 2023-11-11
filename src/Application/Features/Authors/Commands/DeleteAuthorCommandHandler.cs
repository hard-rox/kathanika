using Kathanika.Domain.Exceptions;

namespace Kathanika.Application.Features.Authors.Commands;

public sealed class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
{
    private readonly IAuthorRepository authorRepository;
    private readonly IPublicationRepository publicationRepository;

    public DeleteAuthorCommandHandler(IAuthorRepository authorRepository, IPublicationRepository publicationRepository)
    {
        this.authorRepository = authorRepository;
        this.publicationRepository = publicationRepository;
    }

    public async Task Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        _ = await authorRepository.GetByIdAsync(request.Id, cancellationToken) ??
            throw new NotFoundWithTheIdException(typeof(Author), request.Id);

        bool hasPublication = (await publicationRepository.CountAsync(x => x.Authors.Any(y => y.Id == request.Id), cancellationToken)) > 0;
        if (hasPublication) throw new DeletionFailedException(typeof(Author), "This author has one or more publications in this library.");

        await authorRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
