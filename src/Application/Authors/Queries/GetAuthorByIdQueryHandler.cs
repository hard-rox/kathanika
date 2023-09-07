namespace Kathanika.Application.Authors.Queries;

internal class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Author?>
{
    private readonly IAuthorRepository authorRepository;

    public GetAuthorByIdQueryHandler(IAuthorRepository authorRepository)
    {
        this.authorRepository = authorRepository;
    }

    public async Task<Author?> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var author = await authorRepository.GetByIdAsync(request.Id, cancellationToken);
        return author;
    }
}