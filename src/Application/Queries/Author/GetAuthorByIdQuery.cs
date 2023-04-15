namespace Kathanika.Application.Queries;

public sealed record GetAuthorByIdQuery(
    string Id
) : IRequest<Author>;