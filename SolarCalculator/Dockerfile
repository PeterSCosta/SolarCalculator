FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SolarCalculator.csproj", "."]
RUN dotnet restore "SolarCalculator.csproj"
COPY . .
COPY ["appsettings.json", "WebApp/FileManager.WebApp/"]
WORKDIR "/src/"
RUN dotnet build "SolarCalculator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SolarCalculator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SolarCalculator.dll"]
