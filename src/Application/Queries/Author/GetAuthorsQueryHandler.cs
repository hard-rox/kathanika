namespace Kathanika.Application.Queries;

internal class GetAuthorsQueryHandler : IRequestHandler<GetAuthorsQuery, IEnumerable<Author>>
{
    private readonly IAuthorRepository authorRepository;

    public GetAuthorsQueryHandler(IAuthorRepository authorRepository)
    {
        this.authorRepository = authorRepository;
    }

    public async Task<IEnumerable<Author>> Handle(GetAuthorsQuery request, CancellationToken cancellationToken)
    {
        var authors = await authorRepository.ListAllAsync();
        return authors;
    }
}