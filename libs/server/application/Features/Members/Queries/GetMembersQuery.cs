namespace Kathanika.Application.Features.Members.Queries;

public sealed record GetMembersQuery : IRequest<IQueryable<Member>>;
