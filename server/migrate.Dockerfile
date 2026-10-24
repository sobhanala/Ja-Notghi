FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY . .

RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

WORKDIR /app/Infrastructure

ENTRYPOINT ["dotnet", "ef", "database", "update", "--project", "Infrastructure.csproj", "--startup-project", "../API"]