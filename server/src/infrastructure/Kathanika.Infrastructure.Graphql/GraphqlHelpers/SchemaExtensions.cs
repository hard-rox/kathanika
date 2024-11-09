using HotChocolate.Resolvers;

namespace Kathanika.Infrastructure.Graphql.GraphqlHelpers;

internal static class SchemaExtensions
{
    private static void SetError(this IResolverContext context, IEnumerable<KnError> errors)
    {
        foreach (KnError error in errors)
        {
            context.ReportError(ErrorBuilder.New()
                .SetMessage(error.Message)
                .SetCode(error.Code)
                .SetExtension(nameof(KnError.Description), error.Description)
                .Build());
        }
    }

    internal static TOut? Match<TOut>(
        this Domain.Primitives.Result<TOut> result,
        IResolverContext context
    )
        where TOut : class
    {
        if (!result.IsFailure) return result.Value;

        context.SetError(result.Errors);
        return null;
    }
}