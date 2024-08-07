namespace Kathanika.Core.Application.Features.Authors.Queries;

internal sealed class GetAuthorByIdQueryHandler(IAuthorRepository authorRepository) : IRequestHandler<GetAuthorByIdQuery, Result<Author>>
{
    public async Task<Result<Author>> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        Author? author = await authorRepository.GetByIdAsync(request.Id, cancellationToken);
        if (author is null)
            return Result.Failure<Author>(AuthorAggregateErrors.NotFound(request.Id));

        return Result.Success(author);
    }
}
