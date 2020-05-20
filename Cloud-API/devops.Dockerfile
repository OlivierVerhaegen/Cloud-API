FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /Cloud-API
COPY bin/Release/netcoreapp3.1 .
#ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
ENTRYPOINT ["dotnet", "Cloud-API.dll"]
