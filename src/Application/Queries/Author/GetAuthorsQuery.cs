namespace Kathanika.Application.Queries;

public sealed record GetAuthorsQuery() : IRequest<IQueryable<Author>>;