namespace Kathanika.Application.Features.Members.Queries;

public sealed record GetMemberByIdQuery(string Id) : IRequest<Member?>;
