using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Kathanika.Infrastructure.Persistence.BsonSerializers;

public class AutoGenerateObjectIdSerializer : SerializerBase<string>
{
    public override string Deserialize(BsonDeserializationContext context, BsonDeserializationArgs args)
    {
        return context.Reader.ReadObjectId().ToString();
    }

    public override void Serialize(BsonSerializationContext context, BsonSerializationArgs args, string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            value = ObjectId.GenerateNewId().ToString();
        }

        context.Writer.WriteObjectId(ObjectId.Parse(value));
    }
}