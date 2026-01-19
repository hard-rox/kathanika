using Kathanika.Application;
using Kathanika.Application.Services;
using Kathanika.Infrastructure.Graphql;
using Kathanika.Infrastructure.Persistence;
using Kathanika.Infrastructure.Workers;
using Kathanika.ServiceDefaults;
using Kathanika.Web;
using Kathanika.Web.FileOpsConfigurations;
using Serilog;
using tusdotnet;
using tusdotnet.Helpers;

const string fileServingEndpoint = "/fs";
try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    builder.AddServiceDefaults();

    builder.Services.Configure<ApplicationOptions>(
        builder.Configuration.GetSection(nameof(ApplicationOptions))
    );

    builder.Services
        .AddHttpContextAccessor()
        .AddApplication()
        .AddGraphQlInfrastructure()
        .AddPersistenceInfrastructure(builder.Configuration)
        .AddWorkers(builder.Configuration)
        .AddTus(builder.Configuration);

    if (builder.Environment.IsDevelopment())
    {
        builder.Services.AddCors();
        builder.Services.AddHttpResponseFormatter(true);
    }

    WebApplication app = builder.Build();

    app.MapDefaultEndpoints()
        .UseGraphQlInfrastructure()
        .UseSerilogRequestLogging();

    if (builder.Environment.IsDevelopment())
    {
        app.UseCors(options =>
        {
            options.AllowAnyHeader();
            options.AllowAnyMethod();
            options.SetIsOriginAllowed(_ => true);
            options.AllowCredentials();
            options.WithExposedHeaders(CorsHelper.GetExposedHeaders());
        });
    }
    else
    {
        app.UseStaticFiles();
        app.MapFallbackToFile("index.html");
    }

    app.MapGet("fs/{fileId}", async (string fileId, IFileStore fileStore) =>
    {
        (Stream stream, var contentType) = await fileStore.GetAsync(fileId);
        return Results.File(stream, contentType);
    });

    app.MapTus(fileServingEndpoint, TusConfiguration.TusConfigurationFactory);

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}