using System.Reflection;
using Kathanika.Application.Behaviours;
using Microsoft.Extensions.DependencyInjection;

namespace Kathanika.Application;

public static class DependencyInjector
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Register MediatR for internal use
        services.AddMediatR(config => { config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });

        // Register our abstraction layer - this is the public interface for dispatching commands/queries
        services.AddScoped<IDispatcher, MediatRDispatcher>();

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviours<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);

        return services;
    }
}