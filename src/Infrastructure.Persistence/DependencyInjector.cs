using Kathanika.Domain.Repositories;
using Kathanika.Infrastructure.Persistence.BsonClassMaps;
using Kathanika.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Events;

namespace Kathanika.Infrastructure.Persistence;

public static class DependencyInjector
{
    private static void AddMongoDb(this IServiceCollection services, string? connectionString)
    {
        services.AddSingleton<IMongoDatabase>(f =>
                {
                    var mongoClientSettings = MongoClientSettings.FromConnectionString(connectionString);
                    mongoClientSettings.ClusterConfigurator = cc =>
                    {
                        cc.Subscribe<CommandStartedEvent>(e =>
                    {
                        Console.WriteLine($"CommandStartedEvent\t{e.OperationId}-{e.RequestId}\t{e.CommandName}\t{e.Command.ToJson()}");
                        // Logger.LogDebug(new EventId(Convert.ToInt32(e.OperationId), nameof(CommandStartedEvent)),
                        //  "Request ID \"{RequestId}\" MongoDB Command Started \"{CommandName}\". Command \"{Command}\"",
                        //   e.RequestId, e.CommandName, e.Command.ToJson());
                    });
                        cc.Subscribe<CommandSucceededEvent>(e =>
                        {
                            Console.WriteLine($"CommandSucceededEvent\t{e.OperationId}-{e.RequestId}-{e.ConnectionId}\t{e.CommandName}");
                            // logger.LogDebug(new EventId(Convert.ToInt32(e.OperationId), nameof(CommandSucceededEvent)),
                            //  "Request ID \"{RequestId}\" MongoDB Command Started \"{CommandName}\".",
                            //   e.RequestId, e.CommandName);
                        });
                        cc.Subscribe<CommandFailedEvent>(e =>
                        {
                            Console.WriteLine($"CommandFailedEvent\t{e.OperationId}-{e.RequestId}-{e.ConnectionId}\t{e.CommandName}");
                            // logger.LogDebug(new EventId(Convert.ToInt32(e.OperationId), nameof(CommandFailedEvent)),
                            //  "Request ID \"{RequestId}\" MongoDB Command Started \"{CommandName}\".",
                            //   e.RequestId, e.CommandName);
                        });
                    };
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