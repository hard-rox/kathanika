namespace Kathanika.Application.Features.Publishers.Queries;

#pragma warning disable S2094 // Classes should not be empty
public sealed record GetPublishersQuery : IRequest<IQueryable<Publisher>>;
#pragma warning restore S2094 // Classes should not be empty
