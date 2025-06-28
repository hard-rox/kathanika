# Kathanika Startup Guide

## Overview

Kathanika is a modern, open-source Integrated Library System (ILS) built with Angular 18+ frontend and .NET 8.0/9.0 backend. This guide will help you get started with running and developing the application.

## Prerequisites

- Node.js (latest LTS version)
- npm package manager
- .NET SDK 8.0 or higher
- Git

## Getting Started

### Clone the Repository

```bash
git clone https://github.com/yourusername/kathanika.git
cd kathanika
```

### Install Dependencies

```bash
# Install npm packages
npm install

# Restore .NET tools
dotnet tool restore
```

## Running the Application

### Start the Frontend (Angular)

```bash
ng serve
```

The Angular application will be available at `http://localhost:4200/`.

### Start the Backend (.NET)

```bash
dotnet run --project src/services/Kathanika.Web
```

The .NET backend API will be running on `https://localhost:5001` (or the port specified in your configuration).

## Development Workflow

### Angular Development

- **Start dev server**: `ng serve`
- **Create new component**: `ng generate component components/my-component`
- **Run tests**: `ng test`
- **Run end-to-end tests**: `ng e2e`
- **Build for production**: `ng build --configuration production`

### .NET Development

- **Build solution**: `dotnet build Kathanika.sln`
- **Run with file watching**: `dotnet watch run --project src/services/Kathanika.Web`
- **Run tests**: `dotnet test`
- **Add migration**: `dotnet ef migrations add MigrationName --project src/infrastructure/Kathanika.Infrastructure.Persistence`

## Project Structure

```
/src
  /app-projects     # Angular applications
  /core             # Core domain model and business logic
  /infrastructure   # Infrastructure implementations
  /services         # Backend services (Web API, etc.)
    /Kathanika.Web  # Main Web API project
/tests             # Test projects
/docs              # Documentation
```

## Configuration

### Angular Environment Configuration

Environment configuration files are located in the Angular project's `environments` folder.

### .NET Configuration

The backend configuration is managed through the following files:

- `appsettings.json` - Base configuration
- `appsettings.Development.json` - Development environment overrides
- `appsettings.Production.json` - Production environment overrides

## GraphQL API

Kathanika uses GraphQL for its API layer. When the backend is running, you can access:

- GraphQL Playground: `https://localhost:5001/graphql`
- GraphQL Schema: `https://localhost:5001/graphql/schema`

## Additional Resources

- [Architecture Overview](../../docs/architecture.md)
- [API Documentation](../../docs/api-doc.md)
- [Project Roadmap](../../docs/ROADMAP.md)
- [Contribution Guidelines](../../CONTRIBUTING.md)

## Troubleshooting

### Common Issues

1. **Port conflicts**: If ports are already in use, change them in the respective configuration files.
2. **Database connection**: Ensure your database connection string is correct in `appsettings.json`.
3. **CORS issues**: In development, ensure CORS is configured correctly for your environment.

### Getting Help

If you encounter issues not covered here, please:

1. Check existing GitHub issues
2. Review the documentation
3. Create a new issue with detailed information about your problem

## Deployment

For production deployment instructions, see the [Deployment Guide](../../docs/deployment.md).
