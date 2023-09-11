namespace Kathanika.Application.Authors.Queries;

public sealed record GetAuthorByIdQuery(
    string Id
) : IRequest<Author?>;