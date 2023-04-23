using Kathanika.Application.Services;
using Kathanika.Infrastructure.Services.ServiceImplementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kathanika.Infrastructure.Services;

public static class DependencyInjector
{
    public static IServiceCollection AddServicesInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMemoryCache();
        services.AddScoped<ICacheService, MemoryCacheService>();
        return services;
    }
}
