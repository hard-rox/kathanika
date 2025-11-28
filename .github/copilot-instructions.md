# Copilot Instructions for Kathanika

Kathanika is an open-source Integrated Library System (ILS) with a full-stack architecture: Angular 20 frontend, .NET 10 backend, GraphQL API, and MongoDB persistence.

## Architecture Overview

**Layered CQRS + DDD Architecture:**
- **Domain Layer** (`src/core/Kathanika.Domain`): Aggregates, entities, value objects, repository interfaces. Uses sealed classes with private constructors; business logic lives here.
- **Application Layer** (`src/core/Kathanika.Application`): CQRS commands/queries with handlers, FluentValidation validators, domain event handlers using MediatR pipeline behaviors.
- **Infrastructure Layers**:
  - `Infrastructure.Persistence`: MongoDB repository implementations
  - `Infrastructure.Graphql`: GraphQL schema, resolvers (HotChocolate), query/mutation mappings to application layer
  - `Infrastructure.Workers`: Background services for async tasks
- **Web** (`src/services/Kathanika.Web`): ASP.NET Core entry point with Aspire service defaults, TUS file upload, CORS, static file serving
- **Frontend** (`src/app-projects/ils-web`): Angular 20, Apollo GraphQL client, Tailwind CSS, feature-based organization

## Key Development Workflows

**Backend Build & Test:**
```bash
dotnet build                           # Build solution
dotnet test --collect:"XPlat Code Coverage"  # Run xUnit tests with coverage
dotnet format --verify-no-changes      # Lint check
dotnet watch run --project src/services/Kathanika.Web/Kathanika.Web.csproj  # Dev watch mode
```

**Frontend Build & Test:**
```bash
npm install                            # Install dependencies
npm run build                          # Build both kn-ui library and ils-web app
npm run test                           # Jest unit tests
npm run lint                           # ESLint check
npm run codegen                        # Generate TypeScript from GraphQL schema
npm run cy:open                        # Open Cypress E2E tests
npm start                              # Dev server on localhost:4200
```

**Monorepo Structure:** The solution uses Kathanika.sln for .NET projects and an npm monorepo setup with multiple Angular projects in `src/app-projects/`.

## Critical Patterns & Conventions

### Backend (.NET)

**CQRS Commands/Queries:**
- Commands live in `Features/{Feature}/Commands/` with handler, validator, and record classes
- Example structure: `AddVendorCommand.cs` (request record), `AddVendorCommandHandler.cs` (implements `IRequestHandler<>`), `AddVendorCommandValidator.cs` (extends `AbstractValidator<>`)
- MediatR is registered via `AddMediatR()` in `DependencyInjector.cs` with pipeline behaviors for validation
- Always create validators using FluentValidation in the same feature folder

**Domain Aggregates:**
- Located in `Domain/Aggregates/{AggregateRoot}/` (e.g., `VendorAggregate`, `BibRecordAggregate`)
- Aggregates use sealed classes with private parameterless constructors (required for MongoDB)
- All properties are private setters; factory methods like `Create()` or `Factory.Create()` instantiate them
- Exceptions are domain-specific (e.g., `VendorAggregateErrors.InvalidPhoneNumber()`)
- Implement `IRepository<T>` interface in domain; `Infrastructure.Persistence` provides the MongoDB implementation

**Event Sourcing & Domain Events:**
- Domain events live in `Domain/DomainEvents/`
- Event handlers are in `Application/EventHandlers/` and registered via MediatR
- Use records for domain events to ensure immutability

**Validation:** Use FluentValidation validators in Application layer; MediatR pipeline behavior (`ValidationPipelineBehaviours`) automatically validates before handler execution.

### Frontend (Angular)

**Project Structure:**
- `kn-ui`: Reusable component library (published via ng-packagr)
- `ils-web`: Main application consuming `kn-ui`
- Feature-based organization: `features/{feature}` folders for pages/modules
- Shared components/services in `shared/`
- GraphQL operations in `graphql/` with codegen-generated types

**Testing:**
- Use Jest (configured in `jest.config.ts`), NOT Karma/Jasmine
- Test files colocate with source: `component.ts` → `component.spec.ts`
- Generate spec files via `ng generate component` (do NOT manually create)
- Test template behavior and public API; do NOT test private methods directly
- Avoid `any` types in tests

**GraphQL Client:**
- Apollo Angular 11 with codegen (`src/app-projects/ils-web/codegen.ts`)
- Run `npm run codegen` after GraphQL schema changes to regenerate TypeScript types
- Queries/mutations are strongly typed from generated types

### Shared Conventions

**Commit Messages:** Follow [Conventional Commits](COMMIT_CONVENTION.md):
- Types: `feat`, `fix`, `docs`, `style`, `refactor`, `perf`, `test`, `build`, `ci`, `chore`, `revert`
- Scopes: `core`, `web`, `api`, `db`, `ui`, `auth`, `test`, `deps`
- Format: `type(scope): description` with optional body and footer referencing issues

**Testing Requirements:**
- Backend: xUnit tests in `tests/` mirroring `src/` structure (see `aiguideline.md`)
  - Minimum 3 test cases: happy path, edge case, failure case
- Frontend: Jest specs colocate with components
  - Minimum: 1 happy path, 1 edge case, 1 failure case, 1 template test

**Code Quality:**
- SonarCloud scans: Backend on `kathanika-server-sln`, Frontend on `ts-projects`
- Coverage reports uploaded in CI/CD pipeline
- Pre-commit hooks (Husky) enforce linting

## Critical Files & Integration Points

- **Dependency Injection**: `src/core/Kathanika.Application/DependencyInjector.cs` — register MediatR, validators, handlers
- **Service Registration**: `src/services/Kathanika.Web/Program.cs` — add layers via extension methods (`.AddApplication()`, `.AddPersistenceInfrastructure()`, etc.)
- **GraphQL Resolvers**: `src/infrastructure/Kathanika.Infrastructure.Graphql/` — map queries/mutations to application layer commands/queries
- **Repository Pattern**: Define in Domain, implement in `Infrastructure.Persistence` (MongoDB)
- **Configuration**: `appsettings.json`, environment-specific overrides in CI/CD workflows
- **Frontend Routing**: `src/app-projects/ils-web/src/app/app.routes.ts` — feature route imports

## Common Pitfalls to Avoid

1. **Domain Logic in GraphQL Resolvers**: Business logic belongs in domain aggregates/application handlers, not resolvers
2. **Missing Validators**: Every command should have a corresponding `*Validator` class; validators are auto-registered via reflection in `DependencyInjector`
3. **Mutable Aggregates**: Domain aggregates must be sealed with private setters; use factory methods to construct
4. **Testing with `any` in TypeScript**: Use proper types generated from GraphQL codegen
5. **Hardcoded Configuration**: Use `appsettings.json` and dependency injection; never hardcode values
6. **Missing Tests on Test Changes**: If you modify test data or existing tests, ensure all tests still pass before committing

## Build & CI/CD

**Local Development:**
- Backend watch: Run VS Code task "watch" or `dotnet watch run`
- Frontend dev: `npm start` launches dev server with hot reload
- Both serve simultaneously on `localhost:4200` (frontend) and `https://localhost:7167` (GraphQL backend)

**CI/CD Pipeline** (`.github/workflows/ci.yml`):
- Linting: `dotnet format --verify-no-changes` and `npm run lint`
- Build: `dotnet build` and `npm run build`
- Test: `dotnet test` and `npm run test` with coverage collection
- Sonar analysis uploads coverage reports from both layers

**Docker:**
- Tasks available: `docker-build: debug` and `docker-build: release`
- Dockerfile targets: `base` for debug, production-optimized for release
- Use `docker-run: debug` or `docker-run: release` tasks for local testing

## Documentation to Reference

- [Architecture Overview](docs/architecture.md) — layers, technology stack, deployment
- [Getting Started](docs/getting-started.md) — detailed setup for new developers
- [Contribution Guidelines](CONTRIBUTING.md) — workflow, branch strategy, code review process
- [Product Requirements](PRD.md) — feature set and use cases
- [AI Guidelines](aiguideline.md) — testing, documentation, AI behavior rules for this project
