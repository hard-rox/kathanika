namespace Kathanika.Application.Features.Authors.Queries;

public sealed record GetAuthorByIdQuery(
    string Id
) : IRequest<Author?>;
