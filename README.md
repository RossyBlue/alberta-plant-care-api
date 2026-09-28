# 🪴 Alberta Winter Plant Care API

A lightweight RESTful Web API built with **.NET 8**, **C#**, and **Docker** designed to track indoor houseplants and manage special care alerts during Alberta's harsh winter conditions (extreme dry air, reduced daylight, and furnace hazards).

🚀 Tech Stack
Framework: .NET 8 Web API (C#)

Containerization: Docker & Docker Compose

Database: PostgreSQL 16

Documentation: OpenAPI / Swagger UI

Architecture: Controller-Repository Pattern

⚡ Features
Plant Inventory Management: CRUD operations to track indoor plants and their location.

Alberta Winter Care Alerts (/api/plants/alberta-winter-care): Filters plants requiring extra care, such as high-humidity needs (<20% humidity indoors) or artificial grow lights during winter months.

Cold Draft Risk Detection: Automatic property calculation checking if a plant is located near cold windows or draft zones.

🛠️ How to Run with Docker
Make sure you have Docker Desktop installed and running.

1. Clone the repository:
git clone [https://github.com/rossyblue/alberta-plant-care-api.git]
cd alberta-plant-care-api

2. Spin up the environment:
docker compose up --build

3. Access the API Documentation:
Open your browser and navigate to:
👉 http://localhost:8080/swagger

🔌 API Endpoints
Method,Endpoint,Description
GET,/api/plants,Retrieve all indoor plants in the inventory
GET,/api/plants/{id},Get specific plant details by ID
GET,/api/plants/alberta-winter-care,List plants needing humidifiers or grow lights
POST,/api/plants,Add a new plant to the inventory

