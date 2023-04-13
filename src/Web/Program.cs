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
            .ReadFrom.Services(services);
    });

    builder.Services.AddPersistence(builder.Configuration);

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    app.MapGet("/", ([FromServices] IBookRepository repository) =>
    {
        return repository.ListAllAsync();
    });
    app.MapPost("/", ([FromServices] IBookRepository repository) =>
    {
        return repository.AddAsync(new Book(DateTime.UtcNow.ToString()));
    });
    app.MapGet("/{id}", ([FromServices] IBookRepository repository, [FromRoute] string id) =>
    {
        return repository.GetByIdAsync(id);
    });
    app.MapPut("/{id}", async ([FromServices] IBookRepository repository, [FromRoute] string id) =>
    {
        var entity = await repository.GetByIdAsync(id);
        entity.Title = "Updated at: " + DateTime.UtcNow.ToString();
        return repository.UpdateAsync(id, entity);
    });

    app.MapDelete("/{id}", ([FromServices] IBookRepository repository, [FromRoute] string id) =>
    {
        return repository.DeleteAsync(id);
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