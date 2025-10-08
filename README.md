# WarfighterSim (SQL Server Edition) — Starter

Minimal walking-skeleton to demo monolith → microservices using **SQL Server**, **Docker Compose**, and **ASP.NET Core 9**.

## Services
- **MissionService** — CRUD `/missions`
- **UnitService** — CRUD `/units`
- **SimulationService** — `POST /simulate?missionId=...`
- **ApiGateway** — proxies `/api/*` to services

## Quick start (Docker Compose)
```bash
docker compose up --build -d
# API Gateway
curl http://localhost:8080/api/missions
curl -X POST "http://localhost:8080/api/simulate?missionId=1"
```
Login to SQL Server: `localhost,1433`  (sa / YourStr0ng!Pass)

> On first run, each service applies EF migrations and seeds data automatically.
