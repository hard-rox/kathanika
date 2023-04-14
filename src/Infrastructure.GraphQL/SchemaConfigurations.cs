using HotChocolate.Execution.Configuration;
using HotChocolate.Types.Pagination;
using Kathanika.Infrastructure.GraphQL.Schema;
using Microsoft.Extensions.DependencyInjection;

namespace Kathanika.Infrastructure.GraphQL;

internal static class SchemaConfigurations
{
    private static IRequestExecutorBuilder AddTypes(this IRequestExecutorBuilder builder)
    {
        return builder;
            //.AddType<SponsorType>();
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
            .AddQueryType<Queries>();
        // .AddMutationType<Mutations>();
        //.AddTypes()
        //.AddInputs()
        //.AddProjections()
        //.AddFiltering()
        //.AddSorting()
        //.ModifyRequestOptions(opt =>
        //{
        //    opt.IncludeExceptionDetails = true;
        //    //opt.TracingPreference = TracingPreference.Always;
        //})
        //.ModifyOptions(opt =>
        //{
        //    opt.SortFieldsByName = true;
        //})
        //.SetPagingOptions(new PagingOptions
        //{
        //    MaxPageSize = 100,
        //    DefaultPageSize = 10,
        //    IncludeTotalCount = true,
        //})
        //.BindRuntimeType<DateTime, DateTimeType>()
        //.BindRuntimeType<Guid, UuidType>()
        //.InitializeOnStartup();
    }

}
