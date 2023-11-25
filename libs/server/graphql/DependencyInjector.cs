using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Kathanika.Infrastructure.GraphQL;

public static class DependencyInjector
{
    public static IServiceCollection AddGraphQLInfrastructure(this IServiceCollection services)
    {
        services.BuildGraphQLSchema();

        return services;
    }

    public static void UseGraphQLInfrastructure(this WebApplication app)
    {
        app.MapGraphQL();
    }
}
