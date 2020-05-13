# https://hub.docker.com/_/microsoft-dotnet-core
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /Cloud-API

# copy csproj and restore as distinct layers
COPY *.csproj .
RUN dotnet restore

# copy everything else and publish app and libraries
COPY . .
RUN dotnet publish -c release -o /out --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/core/runtime:3.1 AS runtime
WORKDIR /Cloud-API
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "Cloud-API.dll"]