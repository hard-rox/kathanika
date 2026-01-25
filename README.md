# Kathanika

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
[![Quality Gate](https://sonarcloud.io/api/project_badges/measure?project=hard-rox_kathanika&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=hard-rox_kathanika)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=hard-rox_kathanika&metric=coverage)](https://sonarcloud.io/summary/new_code?id=hard-rox_kathanika)

A modern, open-source Integrated Library System built with Angular 20, .NET 10, GraphQL, and MongoDB.

## Features

- 📚 **Catalog Management** - MARC21 bibliographic records, metadata handling
- 🔄 **Circulation** - Check-in/out, renewals, holds management  
- 👥 **Patron Management** - User accounts, authentication, self-service
- 🔍 **Search & Discovery** - Advanced search with SRU protocol support
- 🏢 **Acquisitions** - Vendor management, purchase orders, budgets
- 📦 **Serials** - Periodical tracking and subscriptions
- 📊 **Reports** - Analytics and customizable reporting
- 🔔 **Notifications** - Automated alerts for due dates and holds

## Tech Stack

### Frontend

- Angular 20 (standalone components) + TypeScript 5.9
- Apollo Angular 11 for GraphQL
- Tailwind CSS 4 for styling
- Jest + Cypress for testing

### Backend

- .NET 10 (C# 13) with ASP.NET Core
- HotChocolate 14 GraphQL API
- MongoDB with repository pattern
- CQRS + DDD architecture
- MediatR + FluentValidation
- xUnit testing with coverage

### Architecture

- Layered design: Domain → Application → Infrastructure → Web
- Domain events with MediatR pipeline behaviors
- Sealed aggregates with private constructors
- Factory methods for entity creation
- Native dependency injection

## Project Structure

```text
kathanika/
├── src/
│   ├── app-projects/          # Angular applications
│   │   ├── ils-web/           # Main ILS web application
│   │   └── kn-ui/             # Reusable component library
│   ├── core/                  # Backend core layers
│   │   ├── Kathanika.Domain/         # Domain entities, aggregates, value objects
│   │   └── Kathanika.Application/    # CQRS commands/queries, handlers
│   ├── infrastructure/        # Infrastructure implementations
│   │   ├── Kathanika.Infrastructure.Graphql/      # GraphQL schema & resolvers
│   │   ├── Kathanika.Infrastructure.Persistence/  # MongoDB repositories
│   │   └── Kathanika.Infrastructure.Workers/      # Background services
│   ├── services/
│   │   └── Kathanika.Web/     # ASP.NET Core web service
│   └── aspire/                # .NET Aspire orchestration
├── tests/                     # Test projects (xUnit)
├── docs/                      # Documentation
└── scripts/                   # Build and deployment scripts
```

## Getting Started

### Prerequisites

- **Node.js** 20+ and npm 10+
- *Quick Start

**Prerequisites:** Node.js 20+, .NET SDK 10+, MongoDB 6+

```bash
# Clone and install dependencies
git clone https://github.com/hard-rox/kathanika.git
cd kathanika
npm install
dotnet restore

# Configure MongoDB connection
# Edit src/services/Kathanika.Web/appsettings.json:
# "MongoDB": { "ConnectionString": "mongodb://localhost:27017" }

# Start development (2 terminals)
npm start                  # Frontend: http://localhost:4200
dotnet watch run --project src/services/Kathanika.Web/Kathanika.Web.csproj
                          # Backend: https://localhost:7167
```

**VS Code Tasks:**
- Build: `Ctrl+Shift+B` → `build`
- Watch: Run Task → `watch`  
- Docker: Run Task → `docker-build: debug` or `docker-build: release
```bash
# Start dev server with hot reload
npm start

# Run unit tests
npm run test

# Run tests with coverage
npm run test -- --coverage

# Run linter
npm run lint

# Generate GraphQL types from schema
npm run codegen

# Run E2E tests
npm run cy:open  # Interactive mode
npm run cy:run   # Headless mode

# Build for production
npm run build
```

### Backend Development

```bash
# Run in watch mode
dotnet watch run --project src/services/Kathanika.Web/Kathanika.Web.csproj

# Build solution
dotnet build

**Frontend**
```bash
npm start              # Dev server
npm run test           # Unit tests (Jest)
npm run lint           # ESLint
npm run codegen        # Generate GraphQL types
npm run build          # Production build
npm run cy:open        # E2E tests (Cypress)
```

**Backend**  
```bash
dotnet build                                    # Build solution
dotnet test --collect:"XPlat Code Coverage"    # Tests with coverage
dotnet format --verify-no-changes              # Lint check
dotnet watch run --project src/services/Kathanika.Web/Kathanika.Web.csproj  # Watch mode
```

### Code Generation

```bash
# Angular component
ng generate component features/my-feature/my-component

# Backend command/query (manual)
# Create in src/core/Kathanika.Application/Features/{Feature}/Commands|Queries/
# Pattern: {Action}Command.cs, {Action}CommandHandler.cs, {Action}CommandValidator.cs
- **[Product Requirements](PRD.md)** - Feature specifications and use cases
- **[Roadmap](docs/ROADMAP.md)** - Planned features and milestones
- **[Contribution Guidelines](CONTRIBUTING.md)** - How to contribute to the project
- **[Commit Conventions](COMMIT_CONVENTION.md)** - Git commit message standards

## Contributing

Kathanika welcomes contributions from the community! We follow a structured development process:

1. **Fork & Clone**: Fork the repository and clone it locally
2. **Branch**: Create a feature branch (`git checkout -b feat/my-feature`)
3. **Develop**: Make your changes following our coding standards
4. **Test**: Ensure all tests pass and add new tests for your changes
5. **Commit**: Follow [Conventional Commits](COMMIT_CONVENTION.md) standards
6. **Push**: Push your branch and open a pull request
7. **Review**: Respond to code review feedback

Read our detailed [Contribution Guidelines](CONTRIBUTING.md) before submitting a pull request.

### Code Quality Standards

- **Backend**: Follow C# coding conventions, SOLID principles, and DDD patterns
- **Frontend**: Use Angular style guide, TypeScript strict mode, and reactive patterns
- **Tests**: Minimum 3 test cases per feature (happy path, edge case, failure case)
- **Coverage**: Maintain or improve overall test coverage
- **Linting**: All code must pass linter checks (`dotnet format` and `npm run lint`)

## Deployment

### Docker Deployment

```bash
# Build production image
docker build -t kathanika:latest -f Dockerfile .
**Backend (xUnit)**  
- Unit tests: Domain aggregates, application handlers
- Integration tests: MongoDB repositories
- Architecture tests: Layer dependencies (NetArchTest)

**Frontend (Jest + Cypress)**
- Unit tests: Components, services  
- E2E tests: User workflows

```bash
# Run tests with coverage
dotnet test --collect:"XPlat Code Coverage"   # Backend
npm run test -- --coverage                    # Frontend

# Generate reports
./scripts/generate-coverage-report.sh         # Linux/macOS
.\scripts\generate-coverage-report.ps1        # Windows
```

**Test Requirements:** Minimum 3 cases per feature (happy path, edge case, failure)*Libraries**: The open-source libraries and frameworks that power Kathanika
- **Community**: Librarians and library professionals providing feedback and testing
- **Inspiration**: The library community's commitment to open access and collaboration

## Support & Community

- **Issues**: [GitHub Issues](https://github.com/hard-rox/kathanika/issues) for bug reports and feature requests
- **Discussions**: [GitHub Discussions](https://github.com/hard-rox/kathanika/discussions) for questions and ideas
- **Documentation**: [docs/](docs/) folder for comprehensive guides
[Architecture Overview](docs/architecture.md) - System design and patterns
- [Getting Started Guide](docs/getting-started.md) - Detailed setup
- [API Documentation](docs/api-doc.md) - GraphQL schema reference  
- [Product Requirements](PRD.md) - Feature specifications
- [Roadmap](docs/ROADMAP.md) - Planned features
- [Contributing](CONTRIBUTING.md) - Contribution workflow
- [Commit Conventions](COMMIT_CONVENTION.md) - Git commit
docker build -t kathanika:latest .
docker run -d -p 8080:8080 \
  -e MongoDB__ConnectionString="mongodb://your-host:27017" \
Contributions welcome! Please follow [Conventional Commits](COMMIT_CONVENTION.md) and our [Contributing Guidelines](CONTRIBUTING.md).

**Quick Workflow:**
1. Fork and create feature branch: `git checkout -b feat/my-feature`
2. Make changes following code standards
3. Add tests (minimum 3 cases: happy path, edge case, failure)
4. Commit: `feat(scope): description` format
5. Push and open pull request

**Code Standards:**
- Backend: SOLID, DDD patterns, sealed aggregates with factories
- Frontend: Angular style guide, TypeScript strict, reactive patterns  
- All code must pass `dotnet format --verify-no-changes` and `npm run lint`MIT License - see [LICENSE](LICENSE) for details.

## Support

- [Issues](https://github.com/hard-rox/kathanika/issues) - Bug reports and feature requests
- [Discussions](https://github.com/hard-rox/kathanika/discussions) - Questions and ideas  
- [Documentation](docs/) - C