using Kathanika.Core.Domain.Exceptions;
using Kathanika.Infrastructure.Graphql.Payloads;
using Microsoft.AspNetCore.Mvc;

namespace Kathanika.Infrastructure.Graphql.Schema;

public sealed partial class Mutations
{
    [Error<InvalidFieldException>]
    public static async Task<AddAuthorPayload> AddAuthorAsync([FromServices] IMediator mediator, AddAuthorCommand input)
    {
        Author author = await mediator.Send(input);
        return new(author);
    }

    [Error<InvalidFieldException>]
    [Error<NotFoundWithTheIdException>]
    public static async Task<UpdateAuthorPayload> UpdateAuthorAsync([FromServices] IMediator mediator, UpdateAuthorCommand input)
    {
        Author author = await mediator.Send(input);
        return new(author);
    }

    [Error<NotFoundWithTheIdException>]
    [Error<DeletionFailedException>]
    public static async Task<DeleteAuthorPayload> DeleteAuthorAsync([FromServices] IMediator mediator, string id)
    {
        await mediator.Send(new DeleteAuthorCommand(id));
        return new(id);
    }

    // [GraphQLDeprecated("Just a dummy for throwing new notification...")]
    // public async Task<Notification> FireNewNotification([FromServices]ITopicEventSender eventSender, string content)
    // {
    //     Notification notification = new() { Message = content };
    //     await eventSender.SendAsync("NewNotification", notification);
    //     return notification;
    // }
}
