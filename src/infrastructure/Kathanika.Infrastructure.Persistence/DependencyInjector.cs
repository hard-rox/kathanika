using System.Reflection;
using Azure.Storage.Blobs;
using Kathanika.Application.Services;
using Kathanika.Infrastructure.Persistence.BsonClassMaps;
using Kathanika.Infrastructure.Persistence.Caching;
using Kathanika.Infrastructure.Persistence.FileStorage;
using Kathanika.Infrastructure.Persistence.Outbox;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kathanika.Infrastructure.Persistence;

public static class DependencyInjector
{
    private static void RegisterClassMapFromAssembly()
    {
        Assembly assembly = typeof(IBsonClassMap).Assembly;

        List<Type> classMaps = assembly.GetTypes()
            .Where(t => typeof(IBsonClassMap).IsAssignableFrom(t)
                        && t is { IsInterface: false, IsAbstract: false })
            .ToList();

        classMaps.ForEach(x =>
        {
            IBsonClassMap? classMapInstance = (IBsonClassMap?)Activator.CreateInstance(x);
            classMapInstance?.Register();
        });
    }

    public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        RegisterClassMapFromAssembly();

        var connectionString = configuration.GetConnectionString("mongodb");
        services.AddMongoDb(connectionString);

#pragma warning disable EXTEXP0018
        services.AddHybridCache()
            .AddSerializerFactory<CacheJsonSerializerFactory>();
#pragma warning restore EXTEXP0018
        services.AddStackExchangeRedisCache(opt =>
        {
            opt.Configuration = configuration.GetConnectionString("redis");
        });

        services.AddScoped<IOutboxMessageService, OutboxMessageService>();
        services.AddSingleton<IFileMetadataService, FileMetadataService>();

        // services.AddSingleton<IFileStore, DiskFileStore>();
        services.AddSingleton<IFileStore, AzureBlobStore>();
        services.AddSingleton(_ =>
            new BlobServiceClient(configuration.GetConnectionString("azureBlobStorage")));

        services.RegisterRepositories();

        return services;
    }

    private static void RegisterRepositories(this IServiceCollection services)
    {
        List<Type> types = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.Name.Contains("Repository")
                        && t is { IsClass: true, IsAbstract: false }).ToList();

        foreach (Type type in types)
        {
            Type? interfaceType = type
                .GetInterfaces()
                .FirstOrDefault(x => !x.IsGenericType
                                     && x.Name.Contains(type.Name));
            if (interfaceType != null)
            {
                services.AddScoped(interfaceType, type);
            }
        }
    }
}