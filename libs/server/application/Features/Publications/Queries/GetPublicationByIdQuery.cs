namespace Kathanika.Application.Features.Publications.Queries;

public sealed record GetPublicationByIdQuery(
    string Id
    ) : IRequest<Publication?>;
