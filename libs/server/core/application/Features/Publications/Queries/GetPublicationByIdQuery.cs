namespace Kathanika.Core.Application.Features.Publications.Queries;

public sealed record GetPublicationByIdQuery(
    string Id
    ) : IRequest<Result<Publication>>;
