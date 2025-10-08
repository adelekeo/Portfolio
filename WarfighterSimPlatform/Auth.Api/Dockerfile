FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8081
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . /src/Auth.Api
RUN dotnet restore /src/Auth.Api
RUN dotnet publish /src/Auth.Api -c Release -o /out
FROM base AS final
WORKDIR /app
COPY --from=build /out .
ENV ASPNETCORE_URLS=http://+:8081
ENTRYPOINT ["dotnet","Auth.Api.dll"]
