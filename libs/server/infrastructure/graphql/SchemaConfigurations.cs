using System.Reflection;
using System.Runtime.CompilerServices;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Execution.Configuration;
using HotChocolate.Types.Pagination;
using Kathanika.Infrastructure.Graphql.Schema;
using Microsoft.Extensions.DependencyInjection;

namespace Kathanika.Infrastructure.Graphql;

internal static class SchemaConfigurations
{
    private static Type[] GetTypesFromNamespace(string nameSpace)
    {
        Type[] types = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(type => type.Namespace == nameSpace
                && Attribute.GetCustomAttribute(type, typeof(CompilerGeneratedAttribute)) == null)
            .ToArray();

        return types;
    }

    internal static IRequestExecutorBuilder BuildGraphQLSchema(this IServiceCollection services)
    {
        IRequestExecutorBuilder requestBuilder = services.AddGraphQLServer();
        //requestBuilder.AddAuthorization();
        requestBuilder.AddTypes(GetTypesFromNamespace("Kathanika.Infrastructure.Graphql.Types"));
        requestBuilder.AddTypes(GetTypesFromNamespace("Kathanika.Infrastructure.Graphql.Inputs"));
        requestBuilder.AddTypes(GetTypesFromNamespace("Kathanika.Infrastructure.Graphql.Schema"));
        requestBuilder.AddQueryType(q => q.Name(OperationTypeNames.Query));
        requestBuilder.AddMutationType(m => m.Name(OperationTypeNames.Mutation));
        requestBuilder.AddSubscriptionType<Subscriptions>();
        requestBuilder.AddInMemorySubscriptions();
        requestBuilder.AddProjections();
        requestBuilder.AddFiltering();
        requestBuilder.AddSorting();
        requestBuilder.AddErrorFilter(error =>
        {
            Exception? exception = error.Exception;
            IError errorResult = error
                    .RemoveException()
                    .RemoveExtensions()
                    .RemoveLocations();
            if (exception is not null && exception.Source == "MongoDB.Driver.Core")
            {
                return errorResult
                    .WithMessage("Looks like MongoDB is offline or connection string is invalid. Make sure database is online to enjoy.");
            }
            if (error.Exception is not null && error.Exception is not DomainException)
            {
                return errorResult
                    .WithMessage("Something went terribly wrong. We are trying to fix it...");
            }
            return error;
        });
        requestBuilder.ModifyRequestOptions(opt =>
        {
            opt.IncludeExceptionDetails = true;
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
        requestBuilder.AddMutationConventions();
        requestBuilder.InitializeOnStartup();

        return requestBuilder;
    }

}
