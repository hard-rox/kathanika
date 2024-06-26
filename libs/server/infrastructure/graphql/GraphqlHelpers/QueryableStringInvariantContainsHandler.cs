using System.Linq.Expressions;
using System.Reflection;
using HotChocolate.Data.Filters;
using HotChocolate.Data.Filters.Expressions;
using HotChocolate.Language;

namespace Kathanika.Infrastructure.Graphql.GraphqlHelpers;

internal sealed class QueryableStringInvariantContainsHandler(InputParser parser) : QueryableStringOperationHandler(parser)
{
    private static readonly MethodInfo _toLower = typeof(string)
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

        if (parsedValue is string parsedStringValue)
        {
            Expression propertyToLower = Expression.Call(property, _toLower);
            return FilterExpressionBuilder.Contains(propertyToLower, parsedStringValue.ToLower());
        }

        throw new InvalidOperationException();
    }
}
