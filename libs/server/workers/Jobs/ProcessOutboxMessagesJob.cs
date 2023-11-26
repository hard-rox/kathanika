using Kathanika.Domain.Primitives;
using Kathanika.Persistence.Outbox;
using MediatR;
using Newtonsoft.Json;
using Quartz;

namespace Kathanika.Workers.Jobs;

[DisallowConcurrentExecution]
internal sealed class ProcessOutboxMessagesJob(IPublisher publisher, IOutboxMessageService outboxMessageService) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        IReadOnlyList<OutboxMessage> outboxMessages = await outboxMessageService
            .GetUnprocessedOutboxMessagesFromDb();

        foreach (OutboxMessage message in outboxMessages)
        {
            IDomainEvent? domainEvent = JsonConvert
                .DeserializeObject<IDomainEvent>(message.Content, new JsonSerializerSettings()
                {
                    TypeNameHandling = TypeNameHandling.All
                });

            if (domainEvent is null) continue;

            try
            {
                await publisher.Publish(domainEvent, context.CancellationToken);
                await outboxMessageService.SetOutboxMessageProcessed(message.Id);
            }
            catch (Exception ex)
            {
                await outboxMessageService.SetOutboxMessageErrors(message.Id, ex);
            }
        }
    }
}
