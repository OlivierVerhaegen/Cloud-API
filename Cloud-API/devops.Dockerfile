FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS runtime
WORKDIR /Cloud-API
#COPY /bin/Release/netcoreapp3.1 .
ENTRYPOINT ["dotnet", "Cloud-API.dll"]
