using System.Reflection;
using Kathanika.Core.Application.Behaviours;
using Microsoft.Extensions.DependencyInjection;

namespace Kathanika.Core.Application;

public static class DependencyInjector
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehaviours<,>));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly(), includeInternalTypes: true);

        return services;
    }
}
