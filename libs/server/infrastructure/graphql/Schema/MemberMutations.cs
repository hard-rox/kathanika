using Kathanika.Infrastructure.Graphql.Payloads;

namespace Kathanika.Infrastructure.Graphql.Schema;

[ExtendObjectType(OperationTypeNames.Mutation)]
public sealed class MemberMutations
{
    public async Task<CreateMemberPayload> CreateMemberAsync(
        [Service] IMediator mediator,
        CreateMemberCommand input,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<Member> result = await mediator.Send(input, cancellationToken);
        return new(result);
    }

    public async Task<UpdateMemberPayload> UpdateMemberAsync(
        [Service] IMediator mediator,
        UpdateMemberCommand input,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<Member> result = await mediator.Send(input, cancellationToken);
        return new(result);
    }

    public async Task<RenewMembershipPayload> RenewMembershipAsync(
        [Service] IMediator mediator,
        string id,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<Member> result = await mediator.Send(new RenewMembershipCommand(id), cancellationToken);
        return new(result);
    }

    public async Task<CancelMembershipPayload> CancelMembershipAsync(
        [Service] IMediator mediator,
        string id,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<Member> result = await mediator.Send(new CancelMembershipCommand(id), cancellationToken);
        return new(result);
    }

    public async Task<SuspendMembershipPayload> SuspendMembershipAsync(
        [Service] IMediator mediator,
        string id,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<Member> result = await mediator.Send(new SuspendMembershipCommand(id), cancellationToken);
        return new(result);
    }
}
