using Serilog;
using Kathanika.Infrastructure.Persistence;
using Kathanika.Domain.Aggregates.Book;
using Microsoft.AspNetCore.Mvc;
using Kathanika.Domain.Repositories;

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
        return repository.AddAsync(new Book(DateTime.UtcNow.ToString()));
    });
    app.MapGet("/add/{id}", ([FromServices] IBookRepository repository, [FromRoute] string id) =>
    {
        return repository.GetByIdAsync(id);
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