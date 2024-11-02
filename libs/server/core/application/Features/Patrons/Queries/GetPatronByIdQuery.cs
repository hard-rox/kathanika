namespace Kathanika.Core.Application.Features.Patrons.Queries;
public sealed record GetPatronByIdQuery(
    string Id
    ) : IRequest<Result<Patron>>;
