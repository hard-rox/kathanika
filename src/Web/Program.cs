using Kathanika.Application;
using Kathanika.Infrastructure.GraphQL;
using Kathanika.Infrastructure.Persistence;
using Kathanika.Infrastructure.Services;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, services, configuration) =>
    {
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services);
    });

    builder.Services.AddApplication()
        .AddGraphQLInfrastructure()
        .AddPersistenceInfrastructure(builder.Configuration)
        .AddServicesInfrastructure(builder.Configuration);

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    app.UseGraphQLInfrastructure();

    app.UseStaticFiles();
    app.MapFallbackToFile("index.html");

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