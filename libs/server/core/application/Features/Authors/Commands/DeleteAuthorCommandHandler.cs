using Kathanika.Core.Domain.Exceptions;

namespace Kathanika.Core.Application.Features.Authors.Commands;

public sealed class DeleteAuthorCommandHandler(IAuthorRepository authorRepository, IPublicationRepository publicationRepository) : IRequestHandler<DeleteAuthorCommand>
{
    public async Task Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        _ = await authorRepository.GetByIdAsync(request.Id, cancellationToken) ??
            throw new NotFoundWithTheIdException(typeof(Author), request.Id);

        bool hasPublication = (await publicationRepository.CountAsync(x => x.Authors.Any(y => y.Id == request.Id), cancellationToken)) > 0;
        if (hasPublication) throw new DeletionFailedException(typeof(Author), "This author has one or more publications in this library.");

        await authorRepository.DeleteAsync(request.Id, cancellationToken);
    }
}
