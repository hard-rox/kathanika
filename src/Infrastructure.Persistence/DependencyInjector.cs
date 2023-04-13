using Kathanika.Domain.Repositories;
using Kathanika.Infrastructure.Persistence.BsonClassMaps;
using Kathanika.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

namespace Kathanika.Infrastructure.Persistence;

public static class DependencyInjector
{
    private static void AddMongoDb(this IServiceCollection services, string? connectionString)
    {
        var conventionPack = new ConventionPack()
        {
            new CamelCaseElementNameConvention(),
            new StringIdStoredAsObjectIdConvention(),
        };
        ConventionRegistry.Register("CamelCaseAndStringIdConventionPack", conventionPack, x => true);

        services.AddSingleton<IMongoDatabase>(f =>
        {
            var sp = services.BuildServiceProvider();
            var logger = sp.GetRequiredService<ILogger<MongoClientSettings>>();

            var mongoClientSettings = MongoClientSettings.FromConnectionString(connectionString);
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
            var mongoClient = new MongoClient(mongoClientSettings);
            var db = mongoClient.GetDatabase("kathanika_book_store");
            return db;
        });
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        ClassMaps.MapClasses();

        var connectionString = configuration.GetConnectionString("mongoDbConnection");
        services.AddMongoDb(connectionString);

        services.AddScoped<IBookRepository, BookRepository>();

        return services;
    }
}