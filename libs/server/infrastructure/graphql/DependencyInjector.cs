using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Kathanika.Infrastructure.Graphql;

public static class DependencyInjector
{
    public static IServiceCollection AddGraphQLInfrastructure(this IServiceCollection services)
    {
        services
            .BuildGraphQLSchema()
            .AddInstrumentation()
            .AddApolloTracing();

        return services;
    }

    public static void UseGraphQLInfrastructure(this WebApplication app)
    {
        app.MapGraphQL();
    }
}
