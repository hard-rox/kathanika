using HotChocolate.Resolvers;

namespace Kathanika.Infrastructure.Graphql.GraphqlHelpers;

internal static class SchemaExtensions
{
    private static void SetError(this IResolverContext context, IEnumerable<KnError> errors)
    {
        foreach (KnError error in errors)
        {
            context.ReportError(ErrorBuilder.New()
                .SetMessage(error.Message ?? "Test Error")
                .SetCode(error.Code ?? "Test ErrorCode")
                .SetExtension(nameof(KnError.Description), error.Description)
                .Build());
        }
    }

    internal static TOut Match<TOut>(
        this Result result,
        IResolverContext context,
        Func<TOut> onSuccess,
        Func<TOut> onFailure
    )
    {
        if (result.IsFailure)
        {
            context.SetError(result.Errors);
            return onFailure();
        }

        return onSuccess();
    }

    internal static TOut Match<TIn, TOut>(
        this Core.Domain.Primitives.Result<TIn> result,
        IResolverContext context,
        Func<TIn, TOut> onSuccess,
        Func<TOut> onFailure
    )
    where TIn : class
    {
        if (result.IsFailure)
        {
            context.SetError(result.Errors);
            return onFailure();
        }

        return onSuccess(result.Value);
    }

    internal static TOut? Match<TOut>(
        this Core.Domain.Primitives.Result<TOut> result,
        IResolverContext context
    )
    where TOut : class
    {
        if (result.IsFailure)
        {
            context.SetError(result.Errors);
            return null;
        }

        return result.Value;
    }
}
