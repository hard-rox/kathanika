using Kathanika.Domain.Primitives;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;

namespace Kathanika.Infrastructure.Persistence.MongoDbConventions;

internal class ValueObjectIdConvention : ConventionBase, IClassMapConvention
{
    public void Apply(BsonClassMap classMap)
    {
        if (classMap.ClassType.BaseType == typeof(ValueObject) 
            && classMap.GetMemberMap("Id") is not null)
        {
            classMap.MapProperty("Id")
                .SetSerializer(new StringSerializer(representation: BsonType.String));
        }
    }
}