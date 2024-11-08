using HotChocolate.Resolvers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Kathanika.Infrastructure.Graphql.GraphqlHelpers;

internal static class FileEndpointResolver
{
    internal static string? ResolveAsFileUrl(IResolverContext context, string? fileId)
    {
        if (fileId is null) return null;
        HttpRequest? request = context.RequestServices
                    .GetRequiredService<IHttpContextAccessor>()
                    .HttpContext?
                    .Request;
        string? scheme = request?.Scheme;
        string? host = request?.Host.ToString();
        return scheme + "://" + host + "/fs/" + fileId;
    }
}
