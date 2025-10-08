FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8091

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Simulation.UI/Simulation.UI.csproj", "Simulation.UI/"]
RUN dotnet restore "Simulation.UI/Simulation.UI.csproj"
COPY . .
WORKDIR /src/Simulation.UI
RUN dotnet publish -c Release -o /out

FROM base AS final
WORKDIR /app
COPY --from=build /out .
ENV ASPNETCORE_URLS=http://+:8091
ENTRYPOINT ["dotnet","Simulation.UI.dll"]

