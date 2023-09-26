namespace Kathanika.Application.Features.Authors.Commands;

internal sealed class AddAuthorCommandHandler : IRequestHandler<AddAuthorCommand, Author>
{
    private readonly IAuthorRepository authorRepository;

    public AddAuthorCommandHandler(IAuthorRepository authorRepository)
    {
        this.authorRepository = authorRepository;
    }

    public async Task<Author> Handle(AddAuthorCommand request, CancellationToken cancellationToken)
    {
        var newAuthor = Author.Create(
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