using Kathanika.Domain.Exceptions;
using Kathanika.Domain.Primitives;

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
        var errors = new List<DomainException>();

        var existingAuthor = await authorRepository.GetByIdAsync(request.Id);

        if (existingAuthor is null)
        {
            errors.Add(new NotFoundWithTheIdException(typeof(Author), request.Id));
        }

        if (errors.Count > 0)
        {
            throw new AggregateException(errors);
        }

        await authorRepository.DeleteAsync(request.Id);
    }
}