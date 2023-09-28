namespace Kathanika.Application.Features.Authors.Queries;

public sealed record GetAuthorsQuery() : IRequest<IQueryable<Author>>;
