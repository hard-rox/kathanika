namespace Kathanika.Application.Publications.Queries;

public sealed record GetPublicationByIdQuery(
    string Id
    ) : IRequest<Publication?>;
