using Kathanika.Domain.Primitives;
using MongoDB.Bson.Serialization;

namespace Kathanika.Infrastructure.Persistence.BsonClassMaps;

internal class DomainEventClassMap : IBsonClassMap
{
    public void Register()
    {
        IEnumerable<Type> domainEventTypes = typeof(IDomainEvent)
            .Assembly
            .GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && typeof(IDomainEvent).IsAssignableFrom(t));

        foreach (Type eventType in domainEventTypes)
        {
            if (BsonClassMap.IsClassMapRegistered(eventType)) continue;
            BsonClassMap cm = new(eventType);
            cm.AutoMap();
            cm.SetIgnoreExtraElements(true);
            cm.SetDiscriminatorIsRequired(true);
            cm.SetDiscriminator(eventType.Name);
            BsonClassMap.RegisterClassMap(cm);
        }
    }
}