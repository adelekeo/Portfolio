FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# copy csproj and restore
COPY ["FirstDocAzApp.csproj", "./"]
RUN dotnet restore "FirstDocAzApp.csproj"

# copy the rest and publish
COPY . .
RUN dotnet publish "FirstDocAzApp.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 8080
ENTRYPOINT ["dotnet", "FirstDocAzApp.dll"]