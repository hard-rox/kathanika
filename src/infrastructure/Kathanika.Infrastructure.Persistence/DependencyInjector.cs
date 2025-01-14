using System.Buffers;
using System.Diagnostics.CodeAnalysis;
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
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;

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

        var connectionString = configuration.GetConnectionString("mongoDbConnection");
        services.AddMongoDb(connectionString);

        services.AddMemoryCache();
        services.AddScoped<ICacheService, MemoryCacheService>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = "192.168.0.221:6379";
        });

#pragma warning disable EXTEXP0018
        services.AddHybridCache()
            .AddSerializerFactory<CacheJsonSerializerFactory>();
#pragma warning restore EXTEXP0018

        services.AddScoped<IOutboxMessageService, OutboxMessageService>();
        services.AddSingleton<IFileMetadataService, FileMetadataService>();

        // services.AddSingleton<IFileStore, DiskFileStore>();
        services.AddSingleton<IFileStore, AzureBlobStore>();
        services.AddSingleton(_ =>
            new BlobServiceClient(configuration.GetConnectionString("azureBlobStorageConnection")));

        services.AddScoped<IPatronRepository, PatronRepository>();
        services.AddScoped<IVendorRepository, VendorRepository>();
        services.AddScoped<IBibRecordRepository, BibRecordRepository>();

        return services;
    }
}

internal class CacheJsonSerializerFactory : IHybridCacheSerializerFactory
{
    public bool TryCreateSerializer<T>([NotNullWhen(true)] out IHybridCacheSerializer<T>? serializer)
    {
        serializer = new CustomJsonSerializer<T>();
        return true;
    }

    private sealed class CustomJsonSerializer<T> : IHybridCacheSerializer<T>
    {
        private static readonly JsonSerializerSettings _jsonSettings = new()
        {
            ContractResolver = new PrivateSetterContractResolver(),
            ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor
        };

        public T Deserialize(ReadOnlySequence<byte> source)
        {
            // Utf8JsonReader reader = new(source);
            // return JsonSerializer.Deserialize<T>(ref reader)!;

            // Convert ReadOnlySequence<byte> to a string for Newtonsoft.Json
            string json = System.Text.Encoding.UTF8.GetString(source.ToArray());

            // Deserialize using the custom settings that allow private members
            return JsonConvert.DeserializeObject<T>(json, _jsonSettings)!;
        }

        public void Serialize(T value, IBufferWriter<byte> target)
        {
            // using Utf8JsonWriter writer = new(target);
            // JsonSerializer.Serialize<T>(writer, value, JsonSerializerOptions.Default);

            // Use a StringWriter to capture JSON output
            using var stringWriter = new StringWriter();

            // Serialize the object to JSON using Newtonsoft.Json
            JsonSerializer serializer = JsonSerializer.Create(_jsonSettings);
            serializer.Serialize(stringWriter, value);

            // Write the serialized JSON to the IBufferWriter<byte>
            var jsonBytes = System.Text.Encoding.UTF8.GetBytes(stringWriter.ToString());
            target.Write(jsonBytes);
        }
    }

    // Custom ContractResolver to handle private setters
    private class PrivateSetterContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (!property.Writable)
            {
                var propertyInfo = member as PropertyInfo;
                if (propertyInfo != null)
                {
                    var hasPrivateSetter = propertyInfo.GetSetMethod(true) != null;
                    property.Writable = hasPrivateSetter;
                }
            }

            return property;
        }
    }
}