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

# --- Before Google Cloud deploy ---
# FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
# WORKDIR /Cloud-API
# # COPY --from=build /app/out .
# COPY /bin/Release/netcoreapp3.1 .
# ENTRYPOINT ["dotnet", "Cloud-API.dll"]

# --- Google Cloud Deploy ---
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app
# Copy csproj and restore as distinct layers
COPY *.csproj ./
RUN dotnet restore
# Copy everything else and build
COPY . ./
RUN find -type d -name bin -prune -exec rm -rf {} \; && find -type d -name obj -prune -exec rm -rf {} \;
RUN dotnet publish -c Release -o out
# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
ENTRYPOINT ["dotnet", "Cloud-API.dll"]