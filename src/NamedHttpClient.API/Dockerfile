FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/NamedHttpClient.API/NamedHttpClient.API.csproj", "src/NamedHttpClient.API/"]
RUN dotnet restore "./src/NamedHttpClient.API/NamedHttpClient.API.csproj"
COPY . .
WORKDIR "/src/src/NamedHttpClient.API"
RUN dotnet build "./NamedHttpClient.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NamedHttpClient.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NamedHttpClient.API.dll"]