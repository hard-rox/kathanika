using Kathanika.Application;
using Kathanika.GraphQL;
using Kathanika.Persistence;
using Kathanika.Workers;
using Serilog;

try
{
    WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, services, configuration) =>
    {
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services);
    });

    builder.Services.AddApplication()
        .AddGraphQLInfrastructure()
        .AddPersistenceInfrastructure(builder.Configuration)
        .AddWorkers(builder.Configuration);

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
        });
    }
    else
    {
        app.UseStaticFiles();
        app.MapFallbackToFile("index.html");
    }

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

public static partial class Program { }
