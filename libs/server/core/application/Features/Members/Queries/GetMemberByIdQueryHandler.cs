
namespace Kathanika.Core.Application.Features.Members.Queries;

internal sealed class GetMemberByIdQueryHandler(IMemberRepository memberRepository) : IRequestHandler<GetMemberByIdQuery, Result<Member>>
{
    public async Task<Result<Member>> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
    {
        Member? member = await memberRepository.GetByIdAsync(request.Id, cancellationToken);

        if (member is null)
            return Result.Failure<Member>(MemberAggregateErrors.NotFound(request.Id));

        return Result.Success(member);
    }
}
