FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["SystemMonitor.Server/SystemMonitor.Server.csproj", "SystemMonitor.Server/"]
RUN dotnet restore "./SystemMonitor.Server/SystemMonitor.Server.csproj"
COPY . .
WORKDIR "/src/SystemMonitor.Server"
RUN dotnet build "./SystemMonitor.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./SystemMonitor.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SystemMonitor.Server.dll"]