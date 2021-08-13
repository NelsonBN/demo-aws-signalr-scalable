#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
# DOCUMENTATION: https://andrewlock.net/creating-a-generalised-docker-image-for-building-asp-net-core-apps-using-onbuild/

FROM mcr.microsoft.com/dotnet/aspnet:5.0.8 AS base-env
WORKDIR /app
EXPOSE 80



# BUILD APPLICATION
FROM mcr.microsoft.com/dotnet/sdk:5.0.302 AS build-env

 # Set workdir as solution folder
WORKDIR /sln

# Copy project
COPY ./*.csproj .

# Restore to cache the layers
RUN dotnet restore ./*.csproj

# Copy all the source code and build
COPY . .

# Build application. Used the "--no-restore" to benefit the layer caches
RUN dotnet build ./*.csproj -c Release --no-restore -o /app/build



# Deploy application. Used the "--no-restore" to benefit the layer caches
FROM build-env AS publish-env
RUN dotnet publish ./*.csproj -c Release --no-restore -o /app/publish



FROM base-env AS final-env
WORKDIR /app
ENV ASPNETCORE_ENVIRONMENT Production
COPY --from=publish-env /app/publish .
ENTRYPOINT ["dotnet", "Demo.AWS.SignalR.Server.dll"]