FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Web/Kathanika.Web.csproj", "src/Web/"]
RUN dotnet restore "src/Web/Kathanika.Web.csproj"
COPY . .
WORKDIR "/src/src/Web"
RUN dotnet build "Kathanika.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Kathanika.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Kathanika.Web.dll"]
