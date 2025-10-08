FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8090
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . /src/Gateway.Api
RUN dotnet restore /src/Gateway.Api
RUN dotnet publish /src/Gateway.Api -c Release -o /out
FROM base AS final
WORKDIR /app
COPY --from=build /out .
ENV ASPNETCORE_URLS=http://+:8090
ENTRYPOINT ["dotnet","Gateway.Api.dll"]