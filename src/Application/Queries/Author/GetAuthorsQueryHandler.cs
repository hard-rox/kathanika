namespace Kathanika.Application.Queries;

internal class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, IQueryable<Author>>
{
    private readonly IAuthorRepository authorRepository;

    public GetAuthorsQueryHandler(IAuthorRepository authorRepository)
    {
        this.authorRepository = authorRepository;
    }

    public async Task<IQueryable<Author>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        var authorsQuery = await Task.Run(() => authorRepository.AsQueryable(), cancellationToken);
        return authorsQuery;
    }
}