using Kathanika.Domain.Repositories;
using Kathanika.Infrastructure.Persistence.BsonClassMaps;
using Kathanika.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Kathanika.Infrastructure.Persistence;

public static class DependencyInjector
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        ClassMaps.MapClasses();

        var connectionString = configuration.GetConnectionString("mongoDbConnection");
        var mongoClient = new MongoClient(connectionString);
        var mongoDatabase = mongoClient.GetDatabase("kathanika_book_store");
        services.AddSingleton(mongoDatabase);

        services.AddScoped<IBookRepository, BookRepository>();
        
        return services;
    }
}