#Stage 0: Base image
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Stage 1: App build
FROM node:24-alpine AS app-build
WORKDIR /app

COPY package.json ./
RUN npm install

COPY angular.json ./
COPY tsconfig.json ./
COPY .postcssrc.json ./
COPY src/app-projects ./src/app-projects

RUN npm run build -- --output-mode static

# Stage 2: Api build
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS api-build
WORKDIR /api
COPY Kathanika.slnx ./
COPY Directory.Build.props ./
COPY Directory.Packages.props ./

COPY src/aspire/Kathanika.AppHost/Kathanika.AppHost.csproj /api/src/aspire/Kathanika.AppHost/Kathanika.AppHost.csproj
COPY src/aspire/Kathanika.ServiceDefaults/Kathanika.ServiceDefaults.csproj /api/src/aspire/Kathanika.ServiceDefaults/Kathanika.ServiceDefaults.csproj

COPY src/core/Kathanika.Domain/Kathanika.Domain.csproj /api/src/core/Kathanika.Domain/Kathanika.Domain.csproj
COPY src/core/Kathanika.Application/Kathanika.Application.csproj /api/src/core/Kathanika.Application/Kathanika.Application.csproj

COPY src/infrastructure/Kathanika.Infrastructure.Graphql/Kathanika.Infrastructure.Graphql.csproj /api/src/infrastructure/Kathanika.Infrastructure.Graphql/Kathanika.Infrastructure.Graphql.csproj
COPY src/infrastructure/Kathanika.Infrastructure.Persistence/Kathanika.Infrastructure.Persistence.csproj /api/src/infrastructure/Kathanika.Infrastructure.Persistence/Kathanika.Infrastructure.Persistence.csproj
COPY src/infrastructure/Kathanika.Infrastructure.Workers/Kathanika.Infrastructure.Workers.csproj /api/src/infrastructure/Kathanika.Infrastructure.Workers/Kathanika.Infrastructure.Workers.csproj

COPY src/services/Kathanika.Web/Kathanika.Web.csproj /api/src/services/Kathanika.Web/Kathanika.Web.csproj

COPY tests/Kathanika.Application.Tests/Kathanika.Application.Tests.csproj /api/tests/Kathanika.Application.Tests/Kathanika.Application.Tests.csproj
COPY tests/Kathanika.Domain.Tests/Kathanika.Domain.Tests.csproj /api/tests/Kathanika.Domain.Tests/Kathanika.Domain.Tests.csproj
COPY tests/Kathanika.Infrastructure.Graphql.Tests/Kathanika.Infrastructure.Graphql.Tests.csproj /api/tests/Kathanika.Infrastructure.Graphql.Tests/Kathanika.Infrastructure.Graphql.Tests.csproj
COPY tests/Kathanika.Infrastructure.Persistence.Tests/Kathanika.Infrastructure.Persistence.Tests.csproj /api/tests/Kathanika.Infrastructure.Persistence.Tests/Kathanika.Infrastructure.Persistence.Tests.csproj
COPY tests/Kathanika.Infrastructure.Workers.Tests/Kathanika.Infrastructure.Workers.Tests.csproj /api/tests/Kathanika.Infrastructure.Workers.Tests/Kathanika.Infrastructure.Workers.Tests.csproj
COPY tests/Kathanika.Web.Tests/Kathanika.Web.Tests.csproj /api/tests/Kathanika.Web.Tests/Kathanika.Web.Tests.csproj

RUN dotnet restore

COPY . .
RUN rm -rf src/app-projects

RUN dotnet build -c Release -o /api/build

# Stage 3: Api publish
FROM api-build AS api-publish
WORKDIR /api
RUN dotnet publish -c Release -o /api/publish /p:UseAppHost=false

# Stage 4: Final image
FROM base AS final
WORKDIR /app
COPY --from=api-publish /api/publish .
COPY --from=app-build /app/dist/ils-web/browser ./wwwroot

ENTRYPOINT ["dotnet", "Kathanika.Web.dll"]