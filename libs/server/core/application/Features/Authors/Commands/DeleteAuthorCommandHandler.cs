namespace Kathanika.Core.Application.Features.Authors.Commands;

public sealed class DeleteAuthorCommandHandler(IAuthorRepository authorRepository, IPublicationRepository publicationRepository) : IRequestHandler<DeleteAuthorCommand, Result>
{
    public async Task<Result> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        Author? author = await authorRepository.GetByIdAsync(request.Id, cancellationToken);

        if (author is null)
            return AuthorAggregateErrors.NotFoundError(request.Id);

        bool hasPublication = (await publicationRepository.CountAsync(x => x.Authors.Any(y => y.Id == request.Id), cancellationToken)) > 0;
        if (hasPublication) return AuthorAggregateErrors.HasPublication;

        await authorRepository.DeleteAsync(request.Id, cancellationToken);
        return Result.Success();
    }
}
