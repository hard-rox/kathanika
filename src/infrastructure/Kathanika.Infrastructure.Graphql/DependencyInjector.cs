using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Kathanika.Infrastructure.Graphql;

public static class DependencyInjector
{
    public static IServiceCollection AddGraphQlInfrastructure(this IServiceCollection services)
    {
        services
            .BuildGraphQlSchema()
            .AddInstrumentation();

        return services;
    }

    public static void UseGraphQlInfrastructure(this WebApplication app)
    {
        app.MapGraphQL();
    }
}