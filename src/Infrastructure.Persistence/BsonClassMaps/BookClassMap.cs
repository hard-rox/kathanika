﻿using Kathanika.Domain.Aggregates.Book;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;

namespace Kathanika.Infrastructure.Persistence.BsonClassMaps;

internal static partial class ClassMaps
{
    internal static void MapClasses()
    {
        BsonClassMap.RegisterClassMap<Book>(cm =>
        {
            cm.AutoMap();
            cm.MapIdMember(x => x.Id)
            .SetIdGenerator(new StringObjectIdGenerator())
            .SetSerializer(new StringSerializer(BsonType.ObjectId));
            cm.SetIgnoreExtraElements(true);
        });
    }
}
