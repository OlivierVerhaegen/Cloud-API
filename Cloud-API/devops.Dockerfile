FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /Cloud-API
COPY bin/Release/netcoreapp3.1 .
#ENV ASPNETCORE_ENVIRONMENT=Development
EXPOSE 8085
ENV ASPNETCORE_URLS=http://*:8085
ENTRYPOINT ["dotnet", "Cloud-API.dll"]
