using Serilog;
using Serilog.Events;
using Kathanika.Infrastructure.Persistence;
using Kathanika.Domain.Aggregates.Book;
using Microsoft.AspNetCore.Mvc;
using Kathanika.Domain.Repositories;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((context, services, configuration) =>
    {
        configuration
            .ReadFrom.Configuration(context.Configuration)
            .ReadFrom.Services(services)
            .Enrich.FromLogContext();
    });

    builder.Services.AddPersistence(builder.Configuration);

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    app.MapGet("/", ([FromServices] IBookRepository repository) =>
    {
        return repository.ListAllAsync();
    });
    app.MapGet("/add", ([FromServices] IBookRepository repository) =>
    {
        return repository.AddAsync(new Book() { Name = DateTime.UtcNow.ToString()});
    });

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