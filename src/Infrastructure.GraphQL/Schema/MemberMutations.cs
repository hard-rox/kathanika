using Kathanika.Domain.Exceptions;
using Kathanika.Infrastructure.GraphQL.Payloads;
using Microsoft.AspNetCore.Mvc;

namespace Kathanika.Infrastructure.GraphQL.Schema;

public sealed partial class Mutations
{
    [Error<InvalidFieldException>]
    public async Task<CreateMemberPayload> CreateMemberAsync([FromServices] IMediator mediator, CreateMemberCommand input)
    {
        Member member = await mediator.Send(input);
        return new(member);
    }

    [Error<InvalidFieldException>]
    public async Task<UpdateMemberPayload> UpdateMemberAsync([FromServices] IMediator mediator, UpdateMemberCommand input)
    {
        Member member = await mediator.Send(input);
        return new(member);
    }

    [Error<InvalidFieldException>]
    public async Task<CreateMemberPayload> RenewMembershipAsync([FromServices] IMediator mediator, string id)
    {
        Member member = await mediator.Send(new RenewMembershipCommand(id));
        return new(member);
    }

    // public async Task<MembershipStatusChangedPayload> CancelMembershipAsync([FromServices] IMediator mediator, string id)
    // {
    //     Member member = await mediator.Send(new CancelMembershipCommand(id));
    //     return new(member);
    // }

    // public async Task<MembershipStatusChangedPayload> SuspendMembershipAsync([FromServices] IMediator mediator, string id)
    // {
    //     Member member = await mediator.Send(new SuspendMembershipCommand(id));
    //     return new(member);
    // }
}
