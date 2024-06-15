namespace Kathanika.Core.Application.Features.Authors.Queries;

#pragma warning disable S2094 // Classes should not be empty
public sealed record GetAuthorsQuery : IRequest<IQueryable<Author>>;
#pragma warning restore S2094 // Classes should not be empty
