using Kathanika.Persistence.MongoDbConventions;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver.Core.Events;

namespace Kathanika.Persistence;

internal static class MongoDbConfigurations
{
    internal static void AddMongoDb(this IServiceCollection services, string? connectionString)
    {
        ConventionPack conventionPack = new()
        {
            new CamelCaseElementNameConvention(),
            new StringIdStoredAsObjectIdConvention(),
            new IgnoreExtraElementsConvention(true),
            new ValueObjectIdConvention(),
        };
        ConventionRegistry.Register("ApplicationConventionPack", conventionPack, x => true);

        services.AddSingleton<IMongoDatabase>(f =>
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
            MongoClient mongoClient = new(mongoClientSettings);
            IMongoDatabase db = mongoClient.GetDatabase("kathanika_book_store");
            return db;
        });
    }
}
