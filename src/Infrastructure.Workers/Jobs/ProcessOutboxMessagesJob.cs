using Kathanika.Domain.Primitives;
using Kathanika.Infrastructure.Persistence.Outbox;
using MediatR;
using Newtonsoft.Json;
using Quartz;

namespace Kathanika.Infrastructure.Workers.Jobs;

[DisallowConcurrentExecution]
internal sealed class ProcessOutboxMessagesJob : IJob
{
    private readonly IPublisher _publisher;
    private readonly IOutboxMessageService _outboxMessageService;

    public ProcessOutboxMessagesJob(IPublisher publisher, IOutboxMessageService outboxMessageService)
    {
        _publisher = publisher;
        _outboxMessageService = outboxMessageService;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        IReadOnlyList<OutboxMessage> outboxMessages = await _outboxMessageService
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
                await _publisher.Publish(domainEvent, context.CancellationToken);
                await _outboxMessageService.SetOutboxMessageProcessed(message.Id);
            }
            catch (Exception ex)
            {
                await _outboxMessageService.SetOutboxMessageErrors(message.Id, ex);
            }
        }
    }
}
