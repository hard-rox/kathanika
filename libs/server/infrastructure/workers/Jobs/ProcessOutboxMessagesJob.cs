using Kathanika.Infrastructure.Persistence.Outbox;
using MediatR;
using Quartz;

namespace Kathanika.Infrastructure.Workers.Jobs;

[DisallowConcurrentExecution]
internal sealed class ProcessOutboxMessagesJob(
    IPublisher publisher,
    IOutboxMessageService outboxMessageService) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        IReadOnlyList<OutboxMessage> outboxMessages = await outboxMessageService
            .GetUnprocessedOutboxMessagesFromDb();

        foreach (OutboxMessage message in outboxMessages)
        {
            try
            {
                await publisher.Publish(message.DomainEvent, context.CancellationToken);
                await outboxMessageService.SetOutboxMessageProcessed(message.Id);
            }
            catch (Exception ex)
            {
                await outboxMessageService.SetOutboxMessageErrors(message.Id, ex);
            }
        }
    }
}
