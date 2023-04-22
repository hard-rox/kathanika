using HotChocolate.Execution.Configuration;
using HotChocolate.Types.Pagination;
using Kathanika.Infrastructure.GraphQL.Inputs;
using Kathanika.Infrastructure.GraphQL.Schema;
using Kathanika.Infrastructure.GraphQL.Types;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Kathanika.Infrastructure.GraphQL;

internal static class SchemaConfigurations
{
    private static Type[] GetTypesFromNamespace(string nameSpace)
    {
        var types = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.Namespace == nameSpace
                && Attribute.GetCustomAttribute(type, typeof(CompilerGeneratedAttribute)) == null)
            .ToArray();

        return types;
    }

    internal static IRequestExecutorBuilder BuildGraphQLSchema(this IServiceCollection services)
    {
        var requestBuilder = services.AddGraphQLServer();
        //requestBuilder.AddAuthorization();
        requestBuilder.AddTypes(GetTypesFromNamespace("Kathanika.Infrastructure.GraphQL.Types"));
        requestBuilder.AddTypes(GetTypesFromNamespace("Kathanika.Infrastructure.GraphQL.Inputs"));
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
        requestBuilder.ModifyOptions(opt =>
        {
            opt.SortFieldsByName = false;
        });
        requestBuilder.BindRuntimeType<DateTime, DateTimeType>();
        requestBuilder.AddMutationConventions();
        requestBuilder.InitializeOnStartup();

        return requestBuilder;
    }

}
