namespace Kathanika.Application.Commands;

internal sealed class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, Author>
{
    private readonly IAuthorRepository authorRepository;

    public CreateAuthorCommandHandler(IAuthorRepository authorRepository)
    {
        this.authorRepository = authorRepository;
    }

    public async Task<Author> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var newAuthor = new Author(
            request.FirstName,
            request.LastName,
            request.DateOfBirth,
            request.DateOfDeath,
            request.Nationality,
            request.Biography
        );

        var savedAuthor = await authorRepository.AddAsync(newAuthor);
        return savedAuthor;
    }
}