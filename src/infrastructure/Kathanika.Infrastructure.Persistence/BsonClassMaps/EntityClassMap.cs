using Kathanika.Domain.Primitives;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace Kathanika.Infrastructure.Persistence.BsonClassMaps;

internal class EntityClassMap : IBsonClassMap
{
    internal class EntityIdSerializer(BsonType bsonType) : StringSerializer(bsonType)
    {
        public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, string value)
        {
            if (string.IsNullOrEmpty(value))
                value = ObjectId.GenerateNewId().ToString();
            base.Serialize(context, args, value);
        }
    }

    public void Register()
    {
        BsonClassMap.RegisterClassMap<Entity>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(x => x.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance)
                .SetSerializer(new EntityIdSerializer(BsonType.ObjectId));
        });
    }
}

