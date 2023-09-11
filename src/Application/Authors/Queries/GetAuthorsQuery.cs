namespace Kathanika.Application.Authors.Queries;

public sealed record GetAuthorsQuery() : IRequest<IQueryable<Author>>;