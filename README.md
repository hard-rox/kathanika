# Kathanika - Open Source Integrated Library System

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)

## Quality Gates

### Backend (.NET)

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=kathanika-server-sln&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=kathanika-server-sln)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=kathanika-server-sln&metric=coverage)](https://sonarcloud.io/summary/new_code?id=kathanika-server-sln)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=kathanika-server-sln&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=kathanika-server-sln)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=kathanika-server-sln&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=kathanika-server-sln)

### Frontend (Angular)

[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=ts-projects&metric=alert_status)](https://sonarcloud.io/summary/new_code?id=ts-projects)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=ts-projects&metric=coverage)](https://sonarcloud.io/summary/new_code?id=ts-projects)
[![Maintainability Rating](https://sonarcloud.io/api/project_badges/measure?project=ts-projects&metric=sqale_rating)](https://sonarcloud.io/summary/new_code?id=ts-projects)
[![Security Rating](https://sonarcloud.io/api/project_badges/measure?project=ts-projects&metric=security_rating)](https://sonarcloud.io/summary/new_code?id=ts-projects)

## About Kathanika

Kathanika is a modern, open-source Integrated Library System (ILS) designed to help libraries of all sizes manage their collections, patrons, and operations efficiently. With a user-friendly interface and robust backend, Kathanika provides a comprehensive solution for library management needs.

## Key Features

- **Catalog Management**: Complete bibliographic management with MARC21 support
- **Circulation**: Efficient check-in/check-out processing, renewals, and holds management
- **Patron Management**: Comprehensive patron tracking and self-service capabilities
- **Reporting**: Advanced analytics and reporting tools
- **Search & Discovery**: Powerful search capabilities with faceted navigation
- **Notifications**: Automated alerts for due dates and available holds

## Technology Stack

### Frontend
- **Framework**: Angular 18+ with TypeScript 5.8+
- **UI Design**: Modern, responsive interface using Tailwind CSS
- **Testing**: Jest and Cypress for comprehensive test coverage

### Backend
- **.NET Core**: Built on .NET 8.0/9.0 with C# 12.0/13.0
- **Architecture**: Domain-Driven Design (DDD) approach for clean, maintainable code
- **API**: GraphQL with HotChocolate GraphQL integration

## Getting Started

For detailed setup instructions, see our [Getting Started Guide](docs/getting-started.md).

### Quick Start

```bash
# Clone the repository
git clone https://github.com/yourusername/kathanika.git
cd kathanika

# Install dependencies
npm install

# Install .NET tools
dotnet tool restore

# Start the development servers
# Terminal 1: Start the Angular frontend
ng serve

# Terminal 2: Start the .NET backend
dotnet run --project src/services/Kathanika.Web
```

Navigate to `http://localhost:4200/` to access the frontend application. The backend API will be running on `https://localhost:7167`.

Alternatively, you can use the provided IDE configurations for VS Code or JetBrains Rider to run both frontend and backend simultaneously.

## Documentation

- [Architecture Overview](docs/architecture.md)
- [API Documentation](docs/api-doc.md)
- [Project Roadmap](docs/ROADMAP.md)
- [Product Requirements](PRD.md)
- [Contribution Guidelines](CONTRIBUTING.md)

## Development

### Development Server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

### Code Scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

### Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

### Running Tests

- **Unit Tests**: Run `ng test` to execute unit tests via Jest
- **End-to-End Tests**: Run `ng e2e` to execute e2e tests via Cypress

## Contributing

Kathanika is an open-source project, and we welcome contributions from the community. Please read our [Contribution Guidelines](CONTRIBUTING.md) before submitting a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- All contributors who have helped build Kathanika
- Libraries and librarians who have provided valuable feedback
- Open-source community for continuous support and inspiration

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 18.2.11.

## Development server

Run `ng serve` for the Angular dev server. Navigate to `http://localhost:4200/`. The application will automatically reload if you change any of the source files.

For the .NET backend, run:
```bash
dotnet run --project src/services/Kathanika.Web
```
