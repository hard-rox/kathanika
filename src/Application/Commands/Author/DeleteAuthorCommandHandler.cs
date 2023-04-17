namespace Kathanika.Application.Commands;

public sealed class DeleteAuthorCommandHandler : IRequestHandler<DeleteAuthorCommand>
{
    private readonly IAuthorRepository authorRepository;

    public DeleteAuthorCommandHandler(IAuthorRepository authorRepository)
    {
        this.authorRepository = authorRepository;
    }

    public async Task Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await authorRepository.GetByIdAsync(request.Id);
        if(author is null)
            throw new Exception("No author found with this Id");

        await authorRepository.DeleteAsync(request.Id);
    }
}