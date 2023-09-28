namespace Kathanika.Application.Features.Authors.Queries;

internal class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, IQueryable<Author>>
{
    private readonly IAuthorRepository authorRepository;

    public GetAuthorsQueryHandler(IAuthorRepository authorRepository)
    {
        this.authorRepository = authorRepository;
    }

    public async Task<IQueryable<Author>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Author> authorsQuery = await Task.Run(() => authorRepository.AsQueryable(), cancellationToken);
        return authorsQuery;
    }
}
