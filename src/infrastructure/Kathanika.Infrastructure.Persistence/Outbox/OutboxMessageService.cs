using Newtonsoft.Json;

namespace Kathanika.Infrastructure.Persistence.Outbox;

//TODO: Should be in good way...
internal sealed class OutboxMessageService(IMongoDatabase mongoDatabase) : IOutboxMessageService
{
    private readonly IMongoCollection<OutboxMessage> _outboxMessageCollection
        = mongoDatabase.GetCollection<OutboxMessage>(Constants.OutboxMessageCollectionName);

    public async Task<IReadOnlyList<OutboxMessage>> GetUnprocessedOutboxMessagesFromDb(int limit = 20, CancellationToken cancellationToken = default)
    {
        FilterDefinition<OutboxMessage> filter = Builders<OutboxMessage>
            .Filter
            .And(
                Builders<OutboxMessage>.Filter.Eq(x => x.ProcessedAt, null),
                Builders<OutboxMessage>.Filter.Lt(x => x.ProcessAttempt, 5) //TODO: ProcessAttempt value will get from appSettings.json.
            );

        List<OutboxMessage>? result = await _outboxMessageCollection
            .Find(filter)
            .Skip(0)
            .Limit(20) //TODO: Limit value will get from appSettings.json.
            .ToListAsync(cancellationToken);

        return result ?? [];
    }

    public async Task SetOutboxMessageErrors(string id, Exception exception)
    {
        var exceptionJson = JsonConvert.SerializeObject(exception,
            new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
        FilterDefinition<OutboxMessage> filter = Builders<OutboxMessage>
            .Filter
            .Eq(x => x.Id, id);
        UpdateDefinition<OutboxMessage> updateDefinition = Builders<OutboxMessage>
            .Update
            .Set(x => x.LastOccurredError, exceptionJson)
            .Inc(x => x.ProcessAttempt, 1);
        UpdateResult result = await _outboxMessageCollection.UpdateOneAsync(filter, updateDefinition);
        if (!result.IsAcknowledged)
        {
            //TODO: Log...
        }
    }

    public async Task SetOutboxMessageProcessed(string id)
    {
        FilterDefinition<OutboxMessage> filter = Builders<OutboxMessage>
            .Filter
            .Eq(x => x.Id, id);
        UpdateDefinition<OutboxMessage> updateDefinition = Builders<OutboxMessage>
            .Update
            .Set(x => x.ProcessedAt, DateTimeOffset.Now)
            .Inc(x => x.ProcessAttempt, 1);
        UpdateResult result = await _outboxMessageCollection.UpdateOneAsync(filter, updateDefinition);
        if (!result.IsAcknowledged)
        {
            //TODO: Log...
        }
    }
}