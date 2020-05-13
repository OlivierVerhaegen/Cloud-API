# We can skip these steps because Jenkins does this for us.
# This means we only need the runtime.
# https://hub.docker.com/_/microsoft-dotnet-core
# FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
# WORKDIR /Cloud-API

# # copy csproj and restore as distinct layers
# COPY *.csproj .
# RUN dotnet restore

# # copy everything else and publish app and libraries
# COPY . .
# RUN dotnet publish -c release -o /out --no-restore

# Get de dotnet core sdk
#FROM mcr.microsoft.com/dotnet/core/sdk:3.1
# Get the .Net core runtime image.
#FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS runtime
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /Cloud-API
# COPY --from=build /app/out .
COPY /bin/Release/netcoreapp3.1 .
ENTRYPOINT ["dotnet", "Cloud-API.dll"]