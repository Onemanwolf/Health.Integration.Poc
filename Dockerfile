FROM mcr.microsoft.com/dotnet/sdk:6.0 AS runtime
WORKDIR /app

ENTRYPOINT ["dotnet", "Health.Integration.Poc.dll"]
COPY . /app