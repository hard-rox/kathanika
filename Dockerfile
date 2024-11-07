FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS api-build
WORKDIR /kathanika-project
# RUN ls
COPY ["Kathanika.sln", "."]
COPY ["src/Application/Kathanika.Application.csproj", "src/Application/"]
COPY ["src/Domain/Kathanika.Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure.GraphQL/Kathanika.Infrastructure.Graphql.csproj", "src/Infrastructure.GraphQL/"]
COPY ["src/Infrastructure.Persistence/Kathanika.Infrastructure.Persistence.csproj", "src/Infrastructure.Persistence/"]
COPY ["src/Infrastructure.Services/Kathanika.Infrastructure.Services.csproj", "src/Infrastructure.Services/"]
COPY ["services/web/Kathanika.Web.csproj", "src/Web/"]
COPY ["tests/UnitTests/Kathanika.UnitTests.csproj", "tests/UnitTests/"]

# RUN ls
RUN dotnet restore "Kathanika.sln"

COPY ["src", "src/"]
# RUN ls
RUN dotnet build "services/web/Kathanika.Web.csproj" -c Release -o /app/build

FROM api-build AS publish
RUN dotnet publish "services/web/Kathanika.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM node AS app-build
WORKDIR /usr/src/app
COPY ["src/Web/kathanika-app/package.json", "."]
# RUN ls
RUN npm install
COPY ["src/Web/kathanika-app", "."]
# RUN ls
RUN npm run build

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=app-build /usr/src/app/dist/kathanika-app ./wwwroot/
# RUN ls
# RUN ls wwwroot
ENTRYPOINT ["dotnet", "Kathanika.Web.dll"]




# clear && docker build . -t kathanika:0.0.1 --no-cache --progress=plain
