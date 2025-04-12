using System.Reflection;
using System.Runtime.CompilerServices;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types.Descriptors;
using Microsoft.Extensions.DependencyInjection;

namespace Kathanika.Infrastructure.Graphql;

internal static class SchemaConfigurations
{
    private static Type[] GetSchemaTypes()
    {
        Type[] types = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.Namespace is not null
                           && type.Namespace.Contains("Kathanika.Infrastructure.Graphql.Schema")
                           && Attribute.GetCustomAttribute(type, typeof(CompilerGeneratedAttribute)) == null)
            .ToArray();

        return types;
    }

    internal static IRequestExecutorBuilder BuildGraphQlSchema(this IServiceCollection services)
    {
        IRequestExecutorBuilder requestBuilder = services.AddGraphQLServer();
        //requestBuilder.AddAuthorization();
        requestBuilder.AddTypes(GetSchemaTypes());
        requestBuilder.TryAddTypeInterceptor<IgnorePublicMethodsTypeInterceptor>();
        requestBuilder.AddQueryType(q => q.Name(OperationTypeNames.Query));
        requestBuilder.AddMutationType(m => m.Name(OperationTypeNames.Mutation));
        requestBuilder.AddConvention<INamingConventions, ApplicationNamingConvention>();
        requestBuilder.AddInMemorySubscriptions();
        requestBuilder.AddProjections();
        requestBuilder.AddFiltering();
        requestBuilder.AddSorting();
        requestBuilder.AddErrorFilter(error =>
        {
            Exception? exception = error.Exception;
            if (exception is not null && (exception.Source?.Contains("MongoDB.Driver") ?? false))
                return new Error("Looks like MongoDB is offline or connection string is invalid. Make sure database is online to enjoy.");

            if (error.Exception is not null && error.Exception is not DomainException)
                return new Error("Something went terribly wrong. We are trying to fix it...");

            return error;
        });
        requestBuilder.ModifyRequestOptions(opt => { opt.IncludeExceptionDetails = true; });
        requestBuilder.ModifyPagingOptions(x =>
        {
            x.DefaultPageSize = 10;
            x.MaxPageSize = 100;
            x.IncludeTotalCount = true;
        });
        requestBuilder.ModifyOptions(opt =>
        {
            opt.SortFieldsByName = false;
            opt.RemoveUnreachableTypes = true;
        });
        requestBuilder.ModifyCostOptions(opt => opt.EnforceCostLimits = false);
        requestBuilder.AddConvention<IFilterConvention>(
            new FilterConventionExtension(
                x => x.AddProviderExtension(
                    new QueryableFilterProviderExtension(
                        y => y.AddFieldHandler<QueryableStringInvariantContainsHandler>()
                    )
                )
            )
        );
        requestBuilder.BindRuntimeType<DateTime, DateTimeType>();
        requestBuilder.BindRuntimeType<DateOnly, DateType>();
        requestBuilder.InitializeOnStartup();

        return requestBuilder;
    }
}