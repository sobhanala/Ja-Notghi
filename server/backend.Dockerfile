FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

COPY ["API/API.csproj", "API/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Application/Application.csproj", "Application/"]

RUN dotnet restore "API/API.csproj"

COPY . .

RUN dotnet publish "API/API.csproj" -c Release -o /app/out


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/out .


ENTRYPOINT ["dotnet", "API.dll"]
EXPOSE 8080