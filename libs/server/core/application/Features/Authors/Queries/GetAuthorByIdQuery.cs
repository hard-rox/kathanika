namespace Kathanika.Core.Application.Features.Authors.Queries;

public sealed record GetAuthorByIdQuery(
    string Id
) : IRequest<Result<Author>>;
