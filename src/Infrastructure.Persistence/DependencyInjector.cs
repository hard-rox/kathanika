using Kathanika.Infrastructure.Persistence.BsonClassMaps;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Kathanika.Infrastructure.Persistence;

public static class DependencyInjector
{
    public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        ClassMaps.MapClasses();

        var connectionString = configuration.GetConnectionString("mongoDbConnection");
        services.AddMongoDb(connectionString);

        return services;
    }
}