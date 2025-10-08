WarfighterSimPlatform 🪖

Enterprise-Grade Warfighter Simulation API | ASP.NET Core 9 • Clean Architecture • EF Core • Docker • Azure-Ready

📌 Overview

WarfighterSimPlatform is a full-stack backend project designed to showcase enterprise-level backend engineering skills using modern .NET 9. Built for realism and scalability, it simulates warfighter training scenarios and mission sessions with clean architecture, containerized deployment, and real-world production patterns — making it a strong portfolio project for cloud-ready, defense-focused software engineering roles.

This project demonstrates how to architect a real API platform that can evolve into a large-scale simulation service — from database persistence and seed data to containerized deployment and OpenAPI documentation.

🧰 Tech Stack
Technology	Purpose
.NET 9 / C# 13	Backend API and application services
ASP.NET Core Web API	RESTful endpoints for simulation entities
Clean Architecture	Separation of concerns and testable design
Entity Framework Core + SQL Server	Database persistence and data modeling
Docker & Docker Compose	Containerization and local cloud emulation
Swagger / OpenAPI	API documentation and live testing
Azure-Ready Configuration	Seamless deployment to cloud environments
🏗️ Architecture

The platform follows the Clean Architecture approach:

WarfighterSimPlatform/
├─ Api/              -> ASP.NET Core Web API layer (controllers, DI, Swagger)
├─ Application/      -> Use cases, DTOs, validation, and business logic
├─ Domain/           -> Core domain entities, value objects, and rules
├─ Infrastructure/   -> EF Core, database context, repositories
└─ docker-compose.yml


✅ Key Benefits:

Highly testable and maintainable code

Ready for CI/CD pipelines and container orchestration

Cleanly separated domain logic for long-term scalability

🚀 Getting Started
Prerequisites

.NET 9 SDK

Docker
 (optional but recommended)

SQL Server (LocalDB or Docker container)

1️⃣ Clone the repository
git clone https://github.com/adelekeo/Portfolio.git
cd Portfolio/WarfighterSimPlatform

2️⃣ Configure the database

In appsettings.Development.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=WarfighterSimDb;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}

3️⃣ Apply migrations & seed data
dotnet ef database update --project src/WarfighterSim.Infrastructure --startup-project src/WarfighterSim.Api


✅ Seed data automatically populates initial entities for an instant demo.

4️⃣ Run the API
dotnet run --project src/WarfighterSim.Api


Visit Swagger UI:

http://localhost:5099/swagger

🐳 Run with Docker (Recommended)
docker compose up --build


API: http://localhost:8091

Swagger UI: http://localhost:8091/swagger

🌱 Example Entities

Warfighter – core user profile for simulations

Scenario – mission configurations and parameters

TrainingSession – execution records with outcomes

Sample endpoints:

GET    /api/warfighters
POST   /api/warfighters
GET    /api/scenarios
POST   /api/sessions

💡 Why I Built This

I built WarfighterSimPlatform to demonstrate how I design and deliver real-world, enterprise-ready backend systems — the kind used in defense, cloud, and enterprise modernization projects. It’s more than a demo: it’s a proof of my ability to design scalable architectures, integrate databases, use containerization, and deliver clean, production-ready codebases.

📚 What I Learned

Designing maintainable systems with Clean Architecture

Building containerized APIs with Docker and Compose

Applying EF Core migrations and seed strategies for realistic data

Documenting and testing APIs with Swagger / OpenAPI

Structuring solutions for future Azure deployment and CI/CD

📦 Future Improvements

JWT Authentication & Role-Based Access Control

CI/CD pipeline with GitHub Actions

Deployment to Azure App Service & Azure SQL

Advanced domain modeling and mission analytics

📫 Contact

Oluwole Adeleke
💼 GitHub Portfolio

📧 oluwoleadeleke76@gmail.com