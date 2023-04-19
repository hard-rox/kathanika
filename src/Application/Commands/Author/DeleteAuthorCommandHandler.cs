using Kathanika.Domain.Exceptions;

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
        _ = await authorRepository.GetByIdAsync(request.Id) ??
            throw new NotFoundWithTheIdException(typeof(Author), request.Id);

        await authorRepository.DeleteAsync(request.Id);
    }
}