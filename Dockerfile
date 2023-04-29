FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /kathanika-project
# RUN ls
COPY ["Kathanika.sln", "."]
COPY ["src/Application/Kathanika.Application.csproj", "src/Application/"]
COPY ["src/Domain/Kathanika.Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure.GraphQL/Kathanika.Infrastructure.GraphQL.csproj", "src/Infrastructure.GraphQL/"]
COPY ["src/Infrastructure.Persistence/Kathanika.Infrastructure.Persistence.csproj", "src/Infrastructure.Persistence/"]
COPY ["src/Infrastructure.Services/Kathanika.Infrastructure.Services.csproj", "src/Infrastructure.Services/"]
COPY ["src/Web/Kathanika.Web.csproj", "src/Web/"]
COPY ["tests/UnitTests/Kathanika.UnitTests.csproj", "tests/UnitTests/"]

# RUN ls
RUN dotnet restore "Kathanika.sln"

COPY ["src", "src/"]
# RUN ls
RUN dotnet build "src/Web/Kathanika.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "src/Web/Kathanika.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kathanika.Web.dll"]
