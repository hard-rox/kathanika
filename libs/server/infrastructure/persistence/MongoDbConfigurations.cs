using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver.Core.Events;

namespace Kathanika.Infrastructure.Persistence;

internal static class MongoDbConfigurations
{
    private static void RegisterConventionPacks()
    {
        ConventionPack conventionPack =
                [
                    new CamelCaseElementNameConvention(),
                    new StringIdStoredAsObjectIdConvention(),
                    new IgnoreExtraElementsConvention(true),
                    new EnumRepresentationConvention(BsonType.String)
                ];
        ConventionRegistry.Register("ApplicationConventionPack", conventionPack, x => true);
    }

    internal static void AddMongoDb(this IServiceCollection services, string? connectionString)
    {
        BsonSerializer.RegisterSerializer(typeof(DateTimeOffset), new DateTimeOffsetSerializer(BsonType.Document));

        RegisterConventionPacks();

        services.AddSingleton(f =>
        {
            ServiceProvider sp = services.BuildServiceProvider();
            ILogger<MongoClientSettings> logger = sp.GetRequiredService<ILogger<MongoClientSettings>>();

            MongoClientSettings mongoClientSettings = MongoClientSettings.FromConnectionString(connectionString);
            mongoClientSettings.ClusterConfigurator = cc =>
            {
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
            mongoClientSettings.ApplicationName = "Kathanika ILS";
            MongoClient mongoClient = new(mongoClientSettings);
            IMongoDatabase db = mongoClient.GetDatabase("kathanika_ils");
            return db;
        });
    }
}
