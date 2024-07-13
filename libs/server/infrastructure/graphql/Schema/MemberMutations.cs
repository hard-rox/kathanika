using Kathanika.Core.Domain.Exceptions;
using Kathanika.Infrastructure.Graphql.Payloads;
using Microsoft.AspNetCore.Mvc;

namespace Kathanika.Infrastructure.Graphql.Schema;

public sealed partial class Mutations
{
    [Error<InvalidFieldException>]
    public static async Task<CreateMemberPayload> CreateMemberAsync([FromServices] IMediator mediator, CreateMemberCommand input)
    {
        Member member = await mediator.Send(input);
        return new(member);
    }

    [Error<InvalidFieldException>]
    public static async Task<UpdateMemberPayload> UpdateMemberAsync([FromServices] IMediator mediator, UpdateMemberCommand input)
    {
        Member member = await mediator.Send(input);
        return new(member);
    }

    [Error<InvalidFieldException>]
    public static async Task<RenewMembershipPayload> RenewMembershipAsync([FromServices] IMediator mediator, string id)
    {
        Member member = await mediator.Send(new RenewMembershipCommand(id));
        return new(member);
    }

    [Error<InvalidFieldException>]
    public static async Task<CancelMembershipPayload> CancelMembershipAsync([FromServices] IMediator mediator, string id)
    {
        Member member = await mediator.Send(new CancelMembershipCommand(id));
        return new(member);
    }

    [Error<InvalidFieldException>]
    public static async Task<SuspendMembershipPayload> SuspendMembershipAsync([FromServices] IMediator mediator, string id)
    {
        Member member = await mediator.Send(new SuspendMembershipCommand(id));
        return new(member);
    }
}
