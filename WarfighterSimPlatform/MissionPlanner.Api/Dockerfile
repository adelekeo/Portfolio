# 1️⃣ Base image for runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# 2️⃣ Build image for compiling the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["MissionPlanner.Api/MissionPlanner.Api.csproj", "MissionPlanner.Api/"]
RUN dotnet restore "MissionPlanner.Api/MissionPlanner.Api.csproj"

# Copy everything and build the project
COPY . .
WORKDIR "/src/MissionPlanner.Api"
RUN dotnet publish "MissionPlanner.Api.csproj" -c Release -o /out

# 3️⃣ Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /out .

# Set the listening URL
ENV ASPNETCORE_URLS=http://+:8080

# Run the app
ENTRYPOINT ["dotnet", "MissionPlanner.Api.dll"]
