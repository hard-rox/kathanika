using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Kathanika.Application;

public static class DependencyInjector
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        return services;
    }
}