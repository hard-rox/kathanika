using Kathanika.Infrastructure.Persistence.FileStorage;
using tusdotnet.Interfaces;
using tusdotnet.Models;

namespace Kathanika.Web.FileOpsConfigurations;

internal static class TusConfiguration
{
    internal static IServiceCollection AddTus(this IServiceCollection services, IConfiguration configuration)
    {
        string tusUploadPath = configuration.GetValue<string>("ApplicationOptions:UploadPath") ?? string.Empty;
        if (!string.IsNullOrWhiteSpace(tusUploadPath) && !Directory.Exists(tusUploadPath))
            Directory.CreateDirectory(tusUploadPath);

        services.AddSingleton<ITusFileIdProvider, ApplicationFileIdProvider>();
        services.AddSingleton<IUploadedStore, TusUploadStore>();
        services.AddSingleton<ITusStore, ApplicationTusDiskStore>();
        services.AddSingleton<DefaultTusConfiguration, ApplicationTusConfig>();

        return services;
    }

    internal static Task<DefaultTusConfiguration> TusConfigurationFactory(HttpContext httpContext)
    {
        DefaultTusConfiguration defaultTusConfiguration = httpContext.RequestServices.GetRequiredService<DefaultTusConfiguration>();
        return Task.FromResult(defaultTusConfiguration);
    }
}
