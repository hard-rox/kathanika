using Kathanika.Domain.Exceptions;

namespace Kathanika.Application.Queries;

internal class GetAuthorByIdQueryHandler : IRequestHandler<GetAuthorByIdQuery, Author>
{
    private readonly IAuthorRepository authorRepository;

    public GetAuthorByIdQueryHandler(IAuthorRepository authorRepository)
    {
        this.authorRepository = authorRepository;
    }

    public async Task<Author> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        var author = await authorRepository.GetByIdAsync(request.Id, cancellationToken);
        if (author is null) throw new NotFoundWithTheIdException(typeof(Author), request.Id);
        return author;
    }
}