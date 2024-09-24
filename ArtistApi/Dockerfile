# Set the base image for the final stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Set the base image for the build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the .csproj file from the correct folder
COPY ./ArtistApi/*.csproj ./ArtistApi/

# Navigate to the correct directory and restore dependencies
WORKDIR /src/ArtistApi
RUN dotnet restore

# Copy the rest of the application files
COPY ./ArtistApi/. .

# Restore the application and clear NuGet cache
RUN dotnet nuget locals all --clear && dotnet restore

# Build the application
RUN dotnet build --configuration Release --no-restore

# Publish the application
RUN dotnet publish --configuration Release --no-restore --output /app/publish

# Final stage: build the runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "YourApp.dll"]
