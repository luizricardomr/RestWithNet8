FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["RestWithNet8.Api/RestWithNet8.Api.csproj", "RestWithNet8.Api/"]
RUN dotnet restore "./RestWithNet8.Api/RestWithNet8.Api.csproj"
COPY . .
WORKDIR "/src/RestWithNet8.Api"
RUN dotnet build "./RestWithNet8.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./RestWithNet8.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestWithNet8.Api.dll"]