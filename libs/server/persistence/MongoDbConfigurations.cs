using Kathanika.Domain.Primitives;
using Kathanika.Persistence.Outbox;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver.Core.Events;
using MongoDB.Driver.Core.Extensions.DiagnosticSources;

namespace Kathanika.Persistence;

internal static class MongoDbConfigurations
{
    private static void RegisterConventionPacks()
    {
        ConventionPack conventionPack =
                [
                    new CamelCaseElementNameConvention(),
                    new IgnoreExtraElementsConvention(true),
                    new EnumRepresentationConvention(BsonType.String)
                ];
        ConventionRegistry.Register("ApplicationConventionPack", conventionPack, x => true);
    }

    private static void RegisterDomainEventClassMap()
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

    internal static void AddMongoDb(this IServiceCollection services, string? connectionString)
    {
        BsonSerializer.RegisterSerializer(typeof(DateTimeOffset), new DateTimeOffsetSerializer(BsonType.Document));

        RegisterConventionPacks();

        RegisterDomainEventClassMap();

        services.AddSingleton(f =>
        {
            ServiceProvider sp = services.BuildServiceProvider();
            ILogger<MongoClientSettings> logger = sp.GetRequiredService<ILogger<MongoClientSettings>>();

            MongoClientSettings mongoClientSettings = MongoClientSettings.FromConnectionString(connectionString);
            mongoClientSettings.ClusterConfigurator = cc =>
            {
                cc.Subscribe(new DiagnosticsActivityEventSubscriber());
                cc.Subscribe<CommandStartedEvent>(e =>
                {
                    logger.LogInformation("MongoDB command {@CommandName} started, {@DBRequestId}, {@OperationId}, {@DatabaseName}, {@CommandText}",
                    e.CommandName, e.RequestId, e.OperationId, e.DatabaseNamespace.DatabaseName, e.Command.ToJson(new JsonWriterSettings() { Indent = true }));
                });
                cc.Subscribe<CommandSucceededEvent>(e =>
                {
                    logger.LogInformation("MongoDB command {@CommandName} executed successfully at {@Duration}, {@DBRequestId}, {@OperationId}",
                    e.CommandName, e.Duration, e.RequestId, e.OperationId);
                });
                cc.Subscribe<CommandFailedEvent>(e =>
                {
                    logger.LogError("MongoDB command {@CommandName} execution failed, {@DBRequestId}, {@OperationId}, {@Error}",
                    e.CommandName, e.RequestId, e.OperationId, e.Failure.ToJson(new JsonWriterSettings() { Indent = true }));
                });
            };
            mongoClientSettings.RetryWrites = true;
            MongoClient mongoClient = new(mongoClientSettings);
            IMongoDatabase db = mongoClient.GetDatabase("kathanika_ils");
            return db;
        });
    }
}
