namespace Kathanika.Core.Application.Features.Patrons.Commands;
internal sealed class DeletePatronCommandHandler(
    IPatronRepository patronRepository)
: IRequestHandler<DeletePatronCommand, Result>
{
    public async Task<Result> Handle(DeletePatronCommand request, CancellationToken cancellationToken)
    {
        if (await patronRepository.GetByIdAsync(request.Id, cancellationToken) is null)
            return PatronAggregateErrors.NotFound(request.Id);

        await patronRepository.DeleteAsync(request.Id, cancellationToken);

        return Result.Success();
    }
}
