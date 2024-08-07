namespace Kathanika.Core.Application.Features.Authors.Queries;

internal sealed class GetAuthorsQueryHandler(IAuthorRepository authorRepository) : IRequestHandler<GetAuthorsQuery, IQueryable<Author>>
{
    public async Task<IQueryable<Author>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Author> authorsQuery = await Task.Run(() => authorRepository.AsQueryable(), cancellationToken);
        return authorsQuery;
    }
}
