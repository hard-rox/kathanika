using System.Reflection;
using Azure.Storage.Blobs;
using Kathanika.Application.Services;
using Kathanika.Domain.Aggregates.BibRecordAggregate;
using Kathanika.Domain.Aggregates.PatronAggregate;
using Kathanika.Domain.Aggregates.VendorAggregate;
using Kathanika.Infrastructure.Persistence.BsonClassMaps;
using Kathanika.Infrastructure.Persistence.Caching;
using Kathanika.Infrastructure.Persistence.FileStorage;
using Kathanika.Infrastructure.Persistence.Outbox;
using Kathanika.Infrastructure.Persistence.Repositories;
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

        var connectionString = configuration.GetConnectionString("mongoDb");
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

        services.AddScoped<IPatronRepository, PatronRepository>();
        services.AddScoped<IVendorRepository, VendorRepository>();
        services.AddScoped<IBibRecordRepository, BibRecordRepository>();

        return services;
    }
}