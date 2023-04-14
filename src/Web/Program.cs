using Serilog;
using Kathanika.Infrastructure.Persistence;
using Kathanika.Infrastructure.GraphQL;
using Kathanika.Application;

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
        .AddPersistenceInfrastructure(builder.Configuration);

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    app.UseGraphQLInfrastructure();

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