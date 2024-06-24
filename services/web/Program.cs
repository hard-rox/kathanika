using Kathanika.Core.Application;
using Kathanika.Infrastructure.Graphql;
using Kathanika.Infrastructure.Persistence;
using Kathanika.Infrastructure.Workers;
using Kathanika.Web.FileOpsConfigurations;
using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using tusdotnet;
using tusdotnet.Helpers;
using tusdotnet.Models;

const string _fileServingEndpoint = "/fs";
try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    ConfigureSerilog(builder.Host);

    builder.Services.AddApplication()
        .AddGraphQLInfrastructure()
        .AddPersistenceInfrastructure(builder.Configuration)
        .AddWorkers(builder.Configuration)
        .AddTus();

    AddOpenTelemetry(builder);

    if (builder.Environment.IsDevelopment())
    {
        builder.Services.AddCors();
    }

    WebApplication app = builder.Build();

    app.UseSerilogRequestLogging();

    app.UseGraphQLInfrastructure();

    if (builder.Environment.IsDevelopment())
    {
        app.UseCors(options =>
        {
            options.AllowAnyHeader();
            options.AllowAnyMethod();
            options.SetIsOriginAllowed(origin => true);
            options.AllowCredentials();
            options.WithExposedHeaders(CorsHelper.GetExposedHeaders());
        });
    }
    else
    {
        app.UseStaticFiles();
        app.MapFallbackToFile("index.html");
    }

    app.MapGet("test", (DefaultTusConfiguration config) =>
    {
        return config.Expiration;
    });

    app.MapTus(_fileServingEndpoint, TusConfiguration.TusConfigurationFactory);

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

void ConfigureSerilog(ConfigureHostBuilder host)
{
    host.UseSerilog((context, services, configuration) =>
    {
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services);
    });
}

void AddOpenTelemetry(WebApplicationBuilder builder)
{
    builder.Services.AddOpenTelemetry()
        .ConfigureResource(resource => resource.AddService("Kathanika-Web-Service"))
        .WithTracing(tracing =>
        {
            tracing
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddMongoDBInstrumentation()
                .AddQuartzInstrumentation()
                .AddHotChocolateInstrumentation();

            tracing
                // .AddConsoleExporter()
                .AddOtlpExporter();
        })
        .WithMetrics(metrics =>
        {
            metrics
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation();

            metrics
                // .AddConsoleExporter()
                .AddOtlpExporter();
        });

    builder.Logging.AddOpenTelemetry(logging => logging.AddOtlpExporter());
}

public static partial class Program { }
