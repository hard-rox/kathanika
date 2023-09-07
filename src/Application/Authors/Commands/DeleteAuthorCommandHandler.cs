using Kathanika.Domain.Exceptions;

namespace Kathanika.Application.Authors.Commands;

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
        _ = await authorRepository.GetByIdAsync(request.Id) ??
            throw new NotFoundWithTheIdException(typeof(Author), request.Id);

        var hasPublication = (await publicationRepository.CountAsync(x => x.Authors.Any(y => y.Id == request.Id))) > 0;
        if(hasPublication) throw new DeletionFailedException(typeof(Author), "This author has multiple publications in this library.");

        await authorRepository.DeleteAsync(request.Id);
    }
}