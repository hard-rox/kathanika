namespace Kathanika.Core.Application.Features.Members.Queries;

public sealed record GetMemberByIdQuery(string Id) : IRequest<Result<Member>>;
