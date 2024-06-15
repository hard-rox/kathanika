namespace Kathanika.Core.Application.Features.Authors.Queries;

internal class GetAuthorByIdQueryHandler(IAuthorRepository authorRepository) : IRequestHandler<GetAuthorByIdQuery, Author?>
{
    public async Task<Author?> Handle(GetAuthorByIdQuery request, CancellationToken cancellationToken)
    {
        Author? author = await authorRepository.GetByIdAsync(request.Id, cancellationToken);
        return author;
    }
}
