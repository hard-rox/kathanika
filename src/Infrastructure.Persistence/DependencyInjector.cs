using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Kathanika.Infrastructure.Persistence;

public static class DependencyInjector
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("mongoDbConnection");
        var mongoClient = new MongoClient(connectionString);
        var mongoDatabase = mongoClient.GetDatabase("kathanika_book_store");
        services.AddSingleton<IMongoDatabase>(mongoDatabase);
        
        return services;
    }
}