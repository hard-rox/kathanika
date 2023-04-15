namespace Kathanika.Application.Queries;

internal class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Author>
{
    private readonly IAuthorRepository authorRepository;

    public GetAuthorByIdQueryHandler(IAuthorRepository authorRepository)
    {
        this.authorRepository = authorRepository;
    }

    public Task<Author> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var author = authorRepository.GetByIdAsync(request.Id);
        return author;
    }
}