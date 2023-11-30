namespace Kathanika.Application.Features.Publications.Queries;

#pragma warning disable S2094 // Classes should not be empty
public sealed record GetPublicationsQuery : IRequest<IQueryable<Publication>>;
#pragma warning restore S2094 // Classes should not be empty
