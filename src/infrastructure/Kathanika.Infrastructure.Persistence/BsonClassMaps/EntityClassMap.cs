using Kathanika.Domain.Primitives;
using Kathanika.Infrastructure.Persistence.BsonSerializers;
using MongoDB.Bson.Serialization;

namespace Kathanika.Infrastructure.Persistence.BsonClassMaps;

internal class EntityClassMap : IBsonClassMap
{
    public void Register()
    {
        BsonClassMap.RegisterClassMap<Entity>(cm =>
        {
            cm.AutoMap();
            cm.MapIdProperty(c => c.Id)
                .SetSerializer(new AutoGenerateObjectIdSerializer());
        });
    }
}