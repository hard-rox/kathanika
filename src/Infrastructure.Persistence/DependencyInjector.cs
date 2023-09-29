using Kathanika.Application.Services;
using Kathanika.Infrastructure.Persistence.BsonClassMaps;
using Kathanika.Infrastructure.Persistence.Caching;
using Kathanika.Infrastructure.Persistence.Outbox;
using Kathanika.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Kathanika.Infrastructure.Persistence;

public static class DependencyInjector
{
    public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        ClassMaps.MapClasses();

        string? connectionString = configuration.GetConnectionString("mongoDbConnection");
        services.AddMongoDb(connectionString);

        services.AddMemoryCache();
        services.AddScoped<ICacheService, MemoryCacheService>();

        services.AddScoped<IOutboxMessageService, OutboxMessageService>();

        services.AddScoped<IAuthorRepository, AuthorRepository>();
        services.AddScoped<IPublicationRepository, PublicationRepository>();
        services.AddScoped<IPublisherRepository, PublisherRepository>();

        return services;
    }
}
