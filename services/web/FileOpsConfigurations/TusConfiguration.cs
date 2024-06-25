using Kathanika.Core.Application.Services;
using Kathanika.Infrastructure.Persistence.FileStorage;
using tusdotnet.Interfaces;
using tusdotnet.Models;
using tusdotnet.Models.Configuration;
using tusdotnet.Models.Expiration;
using tusdotnet.Stores;

namespace Kathanika.Web.FileOpsConfigurations;

internal static class TusConfiguration
{
    private static readonly string _uploadPath = "uploads";
    internal static IServiceCollection AddTus(this IServiceCollection services)
    {
        if (!Directory.Exists(_uploadPath)) Directory.CreateDirectory(_uploadPath);

        DefaultTusConfiguration defaultTusConfiguration = new()
        {
            Store = new TusDiskStore(directoryPath: _uploadPath),
            Expiration = new SlidingExpiration(TimeSpan.FromSeconds(300)),
        };

        services.AddSingleton<ITusFileIdProvider, ApplicationFileIdProvider>();
        services.AddSingleton<IUploadedStore, TusUploadStore>();
        services.AddSingleton(defaultTusConfiguration);

        return services;
    }

    internal static Task<DefaultTusConfiguration> TusConfigurationFactory(HttpContext httpContext)
    {
        ITusFileIdProvider databaseFileIdProvider = httpContext.RequestServices.GetRequiredService<ITusFileIdProvider>();
        IFileStore fileStorageService = httpContext.RequestServices.GetRequiredService<IFileStore>();

        DefaultTusConfiguration defaultTusConfiguration = new()
        {
            Store = new TusDiskStore(
            directoryPath: _uploadPath,
            deletePartialFilesOnConcat: true,
            bufferSize: TusDiskBufferSize.Default,
            fileIdProvider: databaseFileIdProvider),
            MaxAllowedUploadSizeInBytes = 30000000, //Default kestrel allowed size.
            Events = new Events()
            {
                OnFileCompleteAsync = async context =>
                {
                    ITusFile file = await context.GetFileAsync();
                    Stream content = await file.GetContentAsync(context.CancellationToken);
                    long fileSize = content.Length;

                    await fileStorageService.RecordUploadCompletedAsync(file.Id, fileSize);
                }
            }
        };
        return Task.FromResult(defaultTusConfiguration);
    }
}
