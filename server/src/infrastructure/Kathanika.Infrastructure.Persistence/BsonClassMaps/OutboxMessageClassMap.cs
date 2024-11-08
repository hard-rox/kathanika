using Kathanika.Domain.Primitives;
using Kathanika.Infrastructure.Persistence.Outbox;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace Kathanika.Infrastructure.Persistence.BsonClassMaps;

internal class OutboxMessageClassMap : IBsonClassMap
{
    public void Register()
    {
        BsonClassMap.RegisterClassMap<OutboxMessage>(cm =>
        {
            cm.AutoMap();
            cm.MapIdProperty(c => c.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.MapMember(m => m.DomainEvent)
                .SetSerializer(new ImpliedImplementationInterfaceSerializer<IDomainEvent, IDomainEvent>());
        });
    }
}
