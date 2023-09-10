namespace Kathanika.Infrastructure.Persistence.Outbox;

//TODO: Should be in good way...
internal sealed class OutboxMessageService : IOutboxMessageService
{
    private readonly IMongoCollection<OutboxMessage> _outboxMessageCollection;

    public OutboxMessageService(IMongoDatabase mongoDatabase)
    {
        _outboxMessageCollection = mongoDatabase.GetCollection<OutboxMessage>(Constants.OutboxMessageCollectionName);
    }
    public async Task<IReadOnlyList<OutboxMessage>> GetUnprocessedOutboxMessagesFromDb(int limit = 20, CancellationToken cancellationToken = default)
    {
        FilterDefinition<OutboxMessage> filter = Builders<OutboxMessage>
            .Filter
            .Eq(x => x.ProcessedAt, null);

        List<OutboxMessage>? result = await _outboxMessageCollection
            .Find(filter)
            .Skip(0)
            .Limit(20)
            .ToListAsync(cancellationToken);

        return result ?? new();
    }

    public async Task SetOutboxMessageProcessed(string id)
    {
        FilterDefinition<OutboxMessage> filter = Builders<OutboxMessage>
            .Filter
            .Eq(x => x.Id, id);
        UpdateDefinition<OutboxMessage> updateDefinition = Builders<OutboxMessage>
            .Update
            .Set(x => x.ProcessedAt, DateTimeOffset.Now);
        UpdateResult result = await _outboxMessageCollection.UpdateOneAsync(filter, updateDefinition);
    }
}