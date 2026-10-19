FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PlantInventoryApi.csproj", "./"]
RUN dotnet restore "PlantInventoryApi.csproj"
COPY . .
RUN dotnet publish "PlantInventoryApi.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_HTTP_PORTS=8080
ENTRYPOINT ["dotnet", "PlantInventoryApi.dll"]

