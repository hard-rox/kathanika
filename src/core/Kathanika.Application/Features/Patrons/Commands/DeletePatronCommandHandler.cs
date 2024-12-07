using Kathanika.Domain.Aggregates.PatronAggregate;
using Kathanika.Domain.Primitives;

namespace Kathanika.Application.Features.Patrons.Commands;

internal sealed class DeletePatronCommandHandler(
    IPatronRepository patronRepository)
    : IRequestHandler<DeletePatronCommand, KnResult>
{
    public async Task<KnResult> Handle(DeletePatronCommand request, CancellationToken cancellationToken)
    {
        if (await patronRepository.GetByIdAsync(request.Id, cancellationToken) is null)
            return PatronAggregateErrors.NotFound(request.Id);

        await patronRepository.DeleteAsync(request.Id, cancellationToken);

        return KnResult.Success();
    }
}