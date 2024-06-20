using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using tusdotnet;
using tusdotnet.Interfaces;
using tusdotnet.Models;
using tusdotnet.Models.Expiration;
using tusdotnet.Stores;

namespace Kathanika.Infrastructure.FileGateway;

public static class DependencyInjector
{
    private

    private static Task<DefaultTusConfiguration> TusConfigurationFactory(HttpContext httpContext)
    {


        return Task.FromResult(configuration);
    }

    public static void UseFileGatewayInfrastructure(this WebApplication app)
    {




        app.MapGet("/fs/{fileId}", async httpContext =>
{
    string fileId = (string)httpContext.GetRouteValue("fileId")!;
    TusDiskStore store = new(_uploadPath);

    ITusFile file = await store.GetFileAsync(fileId, httpContext.RequestAborted);

    if (file == null)
    {
        httpContext.Response.StatusCode = 404;
        return;
    }

    Stream fileStream = await file.GetContentAsync(httpContext.RequestAborted);
    Dictionary<string, Metadata> metadata = await file.GetMetadataAsync(httpContext.RequestAborted);

    // The tus protocol does not specify any required metadata.
    // "contentType" is metadata that is specific to this domain and is not required.
    httpContext.Response.ContentType = metadata.ContainsKey("contentType")
            ? metadata["contentType"].GetString(System.Text.Encoding.UTF8)
            : "application/octet-stream";

    if (metadata.ContainsKey("name"))
    {
        string name = metadata["name"].GetString(System.Text.Encoding.UTF8);
        httpContext.Response.Headers.Add("Content-Disposition", new[] { $"attachment; filename=\"{name}\"" });
    }

    using Stream stream = await file.GetContentAsync(httpContext.RequestAborted);
    await stream.CopyToAsync(httpContext.Response.Body, httpContext.RequestAborted);
});
    }
}
