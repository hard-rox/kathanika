# CQRS Messaging Abstraction

This directory contains the custom CQRS (Command Query Responsibility Segregation) interfaces that abstract away the MediatR dependency.

## Purpose

The abstraction layer enables:
- **Easy replacement of MediatR**: All MediatR usage is centralized through these interfaces
- **Cleaner architecture**: Business logic depends on our interfaces, not third-party libraries
- **Better testability**: Commands and queries can be tested without MediatR infrastructure

## Interfaces

### ICommand<TResponse>
Represents a command that modifies system state and returns a response.
- **Location**: `ICommand.cs`
- **Usage**: All command records inherit from this
- **Example**: `CreateVendorCommand : ICommand<KnResult<Vendor>>`

### IQuery<TResponse>
Represents a query that reads data without side effects.
- **Location**: `IQuery.cs`
- **Usage**: All query records inherit from this
- **Example**: `GetVendorsQuery : IQuery<IQueryable<Vendor>>`

### ICommandHandler<TCommand, TResponse>
Handles the execution of commands.
- **Location**: `ICommandHandler.cs`
- **Usage**: All command handler classes implement this
- **Example**: `CreateVendorCommandHandler : ICommandHandler<CreateVendorCommand, KnResult<Vendor>>`

### IQueryHandler<TQuery, TResponse>
Handles the execution of queries.
- **Location**: `IQueryHandler.cs`
- **Usage**: All query handler classes implement this
- **Example**: `GetVendorsQueryHandler : IQueryHandler<GetVendorsQuery, IQueryable<Vendor>>`

### IDispatcher
The public interface for dispatching commands and queries from outside the Application layer (e.g., GraphQL resolvers).
- **Location**: `IDispatcher.cs`
- **Usage**: Inject this in GraphQL resolvers instead of `IMediator`
- **Methods**:
  - `Send<TResponse>(ICommand<TResponse> command, CancellationToken)`
  - `Send<TResponse>(IQuery<TResponse> query, CancellationToken)`

## Implementation

### Current Implementation: MediatRDispatcher
The current implementation uses MediatR under the hood:
- **Location**: `MediatRDispatcher.cs`
- **Purpose**: Bridges our custom interfaces to MediatR
- **Registration**: Registered as `IDispatcher` in `DependencyInjector.cs`

### Interface Inheritance
Our interfaces currently inherit from MediatR interfaces to maintain compatibility:
- `ICommand<TResponse>` : `IRequest<TResponse>`
- `IQuery<TResponse>` : `IRequest<TResponse>`
- `ICommandHandler<TCommand, TResponse>` : `IRequestHandler<TCommand, TResponse>`
- `IQueryHandler<TQuery, TResponse>` : `IRequestHandler<TQuery, TResponse>`

This allows:
- Automatic handler discovery by MediatR
- Pipeline behaviors (validation) to work seamlessly
- Gradual migration if needed

## How to Replace MediatR

To replace MediatR with a different mediator library:

1. **Create a new dispatcher implementation**
   ```csharp
   internal sealed class YourDispatcher : IDispatcher
   {
       // Implement Send methods using your new library
   }
   ```

2. **Remove MediatR inheritance from interfaces**
   - Remove `: IRequest<TResponse>` from `ICommand` and `IQuery`
   - Remove `: IRequestHandler<...>` from `ICommandHandler` and `IQueryHandler`
   - Add back the `Handle` method signatures to handler interfaces

3. **Update DependencyInjector.cs**
   ```csharp
   // Replace
   services.AddMediatR(...);
   services.AddScoped<IDispatcher, MediatRDispatcher>();
   
   // With
   services.AddYourMediator(...);
   services.AddScoped<IDispatcher, YourDispatcher>();
   ```

4. **Re-implement validation pipeline behavior** for your new library

## Usage Examples

### In Commands/Queries
```csharp
// Command
public sealed record AddVendorCommand(...) : ICommand<KnResult<Vendor>>;

// Query
public sealed record GetVendorsQuery : IQuery<IQueryable<Vendor>>;
```

### In Handlers
```csharp
internal sealed class AddVendorCommandHandler : ICommandHandler<AddVendorCommand, KnResult<Vendor>>
{
    public async Task<KnResult<Vendor>> Handle(AddVendorCommand command, CancellationToken cancellationToken)
    {
        // Implementation
    }
}
```

### In GraphQL Resolvers
```csharp
public async Task<Vendor> AddVendorAsync(
    [Service] IDispatcher dispatcher,
    AddVendorCommand input,
    CancellationToken cancellationToken)
{
    KnResult<Vendor> result = await dispatcher.Send(input, cancellationToken);
    return result.Match(context);
}
```

## Benefits

1. **Loose Coupling**: Application layer is not directly dependent on MediatR
2. **Testability**: Can mock `IDispatcher` for integration tests
3. **Flexibility**: Easy to swap MediatR with alternatives (e.g., Brighter, Wolverine, custom implementation)
4. **Clear Intent**: `ICommand` vs `IQuery` makes intent explicit
5. **Single Responsibility**: Each handler has one job
6. **Type Safety**: Strongly typed requests and responses

## Migration Notes

All commands, queries, and their handlers have been migrated to use these interfaces. The changes include:

- **Application Layer**: All commands/queries/handlers updated
- **Infrastructure.Graphql**: All resolvers now use `IDispatcher` instead of `IMediator`
- **Validation**: `ValidationPipelineBehaviours` works with both command and query types
- **Registration**: `IDispatcher` registered in `DependencyInjector.cs`

## See Also

- [CQRS Pattern](https://martinfowler.com/bliki/CQRS.html)
- [MediatR Documentation](https://github.com/jbogard/MediatR)
- [Application Layer Architecture](../../docs/architecture.md)
