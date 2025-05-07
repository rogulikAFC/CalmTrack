# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 7062
EXPOSE 5062


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Users.gRPC/Users.gRPC.csproj", "Users.gRPC/"]
COPY ["Users.Application/Users.Application.csproj", "Users.Application/"]
COPY ["Users.Domain/Users.Domain.csproj", "Users.Domain/"]
COPY ["Users.Infrastructure/Users.Infrastructure.csproj", "Users.Infrastructure/"]
RUN dotnet restore "./Users.gRPC/Users.gRPC.csproj"
COPY . .
WORKDIR "/src/Users.gRPC"
RUN dotnet build "./Users.gRPC.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Users.gRPC.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Users.gRPC.dll"]