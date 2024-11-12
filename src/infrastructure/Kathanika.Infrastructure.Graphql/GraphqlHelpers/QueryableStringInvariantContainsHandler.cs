using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Language;

namespace Kathanika.Infrastructure.Graphql.GraphqlHelpers;

[SuppressMessage("ReSharper", "ClassNeverInstantiated.Global")]
internal sealed class QueryableStringInvariantContainsHandler(InputParser parser)
    : QueryableStringOperationHandler(parser)
{
    private static readonly MethodInfo ToLower = typeof(string)
        .GetMethods()
        .Single(
            x => x.Name == nameof(string.ToLower) &&
                 x.GetParameters().Length == 0);

    protected override int Operation => DefaultFilterOperations.Contains;

    public override Expression HandleOperation(QueryableFilterContext context,
        IFilterOperationField field,
        IValueNode value,
        object? parsedValue)
    {
        Expression property = context.GetInstance();

        if (parsedValue is not string parsedStringValue) throw new InvalidOperationException();

        Expression propertyToLower = Expression.Call(property, ToLower);
        return FilterExpressionBuilder.Contains(propertyToLower, parsedStringValue.ToLower());
    }
}