using HotChocolate.Execution.Configuration;
using HotChocolate.Types.Pagination;
using Kathanika.Infrastructure.GraphQL.Inputs;
using Kathanika.Infrastructure.GraphQL.Schema;
using Kathanika.Infrastructure.GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;

namespace Kathanika.Infrastructure.GraphQL;

internal static class SchemaConfigurations
{
    private static IRequestExecutorBuilder AddTypes(this IRequestExecutorBuilder builder)
    {
        builder.AddType<AuthorType>();

        return builder;
    }

    private static IRequestExecutorBuilder AddInputs(this IRequestExecutorBuilder builder)
    {
        builder.AddType<CreateAuthorInput>();
        builder.AddType<UpdateAuthorInput>();

        return builder;
    }

    internal static IRequestExecutorBuilder BuildGraphQLSchema(this IServiceCollection services)
    {
        var requestBuilder = services.AddGraphQLServer();
        //requestBuilder.AddAuthorization();
        requestBuilder.AddTypes();
        requestBuilder.AddInputs();
        requestBuilder.AddQueryType<Queries>();
        requestBuilder.AddMutationType<Mutations>();
        requestBuilder.AddProjections();
        requestBuilder.AddFiltering();
        requestBuilder.AddSorting();
        requestBuilder.ModifyRequestOptions(opt =>
        {
            opt.IncludeExceptionDetails = true;
            //opt.TracingPreference = TracingPreference.Always;
        });
        requestBuilder.SetPagingOptions(new PagingOptions
        {
            MaxPageSize = 100,
            DefaultPageSize = 10,
            IncludeTotalCount = true,
        });
        requestBuilder.BindRuntimeType<DateTime, DateTimeType>();
        requestBuilder.InitializeOnStartup();

        return requestBuilder;
    }

}
