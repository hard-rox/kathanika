using HotChocolate.Resolvers;
using Kathanika.Infrastructure.Graphql.Payloads;

namespace Kathanika.Infrastructure.Graphql.Schema;

[ExtendObjectType(OperationTypeNames.Mutation)]
public sealed class MemberMutations
{
    public async Task<CreateMemberPayload> CreateMemberAsync(
        [Service] IMediator mediator,
        IResolverContext context,
        CancellationToken cancellationToken,
        CreateMemberCommand input
    )
    {
        Core.Domain.Primitives.Result<Member> result = await mediator.Send(input, cancellationToken);
        return result.Match<Member, CreateMemberPayload>(
            context,
            member => new(member),
            () => new(null)
        );
    }

    public async Task<UpdateMemberPayload> UpdateMemberAsync(
        [Service] IMediator mediator,
        IResolverContext context,
        UpdateMemberCommand input,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<Member> result = await mediator.Send(input, cancellationToken);
        return result.Match<Member, UpdateMemberPayload>(
            context,
            member => new(member),
            () => new(null)
        );
    }

    public async Task<RenewMembershipPayload> RenewMembershipAsync(
        [Service] IMediator mediator,
        IResolverContext context,
        string id,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<Member> result = await mediator.Send(new RenewMembershipCommand(id), cancellationToken);
        return result.Match<Member, RenewMembershipPayload>(
            context,
            member => new(member),
            () => new(null)
        );
    }

    public async Task<CancelMembershipPayload> CancelMembershipAsync(
        [Service] IMediator mediator,
        IResolverContext context,
        string id,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<Member> result = await mediator.Send(new CancelMembershipCommand(id), cancellationToken);
        return result.Match<Member, CancelMembershipPayload>(
            context,
            member => new(member),
            () => new(null)
        );
    }

    public async Task<SuspendMembershipPayload> SuspendMembershipAsync(
        [Service] IMediator mediator,
        IResolverContext context,
        string id,
        CancellationToken cancellationToken
    )
    {
        Core.Domain.Primitives.Result<Member> result = await mediator.Send(new SuspendMembershipCommand(id), cancellationToken);
        return result.Match<Member, SuspendMembershipPayload>(
            context,
            member => new(member),
            () => new(null)
        );
    }
}
