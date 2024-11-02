using Azure.Storage.Blobs;
using Kathanika.Core.Application.Services;
using Kathanika.Core.Domain.Aggregates.BibRecordAggregate;
using Kathanika.Core.Domain.Aggregates.VendorAggregate;
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
        System.Reflection.Assembly assembly = typeof(IBsonClassMap).Assembly;

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

    public static IServiceCollection AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        RegisterClassMapFromAssembly();

        string? connectionString = configuration.GetConnectionString("mongoDbConnection");
        services.AddMongoDb(connectionString);

        services.AddMemoryCache();
        services.AddScoped<ICacheService, MemoryCacheService>();

        services.AddScoped<IOutboxMessageService, OutboxMessageService>();
        services.AddSingleton<IFileMetadataService, FileMetadataService>();

        // services.AddSingleton<IFileStore, DiskFileStore>();
        services.AddSingleton<IFileStore, AzureBlobStore>();
        services.AddSingleton(_ => new BlobServiceClient(configuration.GetConnectionString("azureBlobStorageConnection")));

        services.AddScoped<IPublicationRepository, PublicationRepository>();
        services.AddScoped<IPatronRepository, PatronRepository>();
        services.AddScoped<IVendorRepository, VendorRepository>();
        services.AddScoped<IBibRecordRepository, BibRecordRepository>();

        return services;
    }
}
