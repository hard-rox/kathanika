using HotChocolate.Execution.Configuration;
using HotChocolate.Types.Pagination;
using Kathanika.Infrastructure.GraphQL.Schema;
using Kathanika.Infrastructure.GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Kathanika.Infrastructure.GraphQL;

internal static class SchemaConfigurations
{
    private static IRequestExecutorBuilder AddTypes(this IRequestExecutorBuilder builder)
    {
        return builder
            .AddTypes(
                typeof(AuthorType)
            );
    }

    private static IRequestExecutorBuilder AddInputs(this IRequestExecutorBuilder builder)
    {
        return builder;
            //.AddType<EventSponsorFilterInput>();
    }

    internal static IRequestExecutorBuilder BuildGraphQLSchema(this IServiceCollection services)
    {
        return services
            .AddGraphQLServer()
            //.AddAuthorization()
            .AddTypes()
        //.AddInputs()
            .AddQueryType<Queries>()
        // .AddMutationType<Mutations>();
            .AddFiltering()
            .AddSorting()
            .ModifyRequestOptions(opt =>
            {
                opt.IncludeExceptionDetails = true;
                //opt.TracingPreference = TracingPreference.Always;
            })
            .SetPagingOptions(new PagingOptions
            {
                MaxPageSize = 100,
                DefaultPageSize = 10,
                IncludeTotalCount = true,
            })
            .BindRuntimeType<DateTime, DateTimeType>()
            .InitializeOnStartup();
    }

}
