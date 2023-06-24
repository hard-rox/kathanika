namespace Kathanika.Application.Queries;

public sealed record GetPublicationByIdQuery(
    string Id
    ) : IRequest<Publication?>;
