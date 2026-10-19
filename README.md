# 🪴 Alberta Winter Plant Care API

A RESTful Web API for tracking indoor houseplants and generating care alerts for Alberta's harsh winters: extremely dry air, short daylight hours, and cold drafts near windows.

Built with **.NET 8**, **PostgreSQL**, and **Docker**.

![Swagger UI](docs/swagger.png)

## Why this project?

Indoor humidity in Alberta homes can drop below 20% in winter, and many houseplants struggle with it. This API helps answer: *which of my plants need a humidifier, a grow light, or to be moved away from a cold window?*

## Features

- **Plant inventory**: track plants, their location, and their care needs.
- **Winter care alerts**: `GET /api/plants/alberta-winter-care` returns plants that need extra humidity (<20% indoor humidity) or artificial grow lights.
- **Cold draft detection**: a computed property flags plants located near cold windows or draft zones.
- **Interactive docs**: Swagger UI included.

## Tech Stack

| Area | Technology |
|------|------------|
| Framework | .NET 8 Web API (C#) |
| Database | PostgreSQL 16 |
| Containers | Docker & Docker Compose |
| Docs | OpenAPI / Swagger |
| Architecture | Controller-Repository pattern |

## Getting Started

**Requirements:** [Docker Desktop](https://www.docker.com/products/docker-desktop/)

```bash
git clone https://github.com/rossyblue/alberta-plant-care-api.git
cd alberta-plant-care-api
docker compose up --build
```

Then open **http://localhost:8080/swagger**.

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/plants` | List all plants |
| GET | `/api/plants/{id}` | Get a plant by ID |
| GET | `/api/plants/alberta-winter-care` | Plants needing humidifiers or grow lights |
| POST | `/api/plants` | Add a new plant |

### Example request

```bash
curl -X POST http://localhost:8080/api/plants \
  -H "Content-Type: application/json" \
  -d '{
    "name": "Monstera",
    "location": "Living room, next to window",
    "needsHumidity": true,
    "needsGrowLight": false
  }'
```

### Example response

```json
{
  "id": 1,
  "name": "Monstera",
  "location": "Living room, next to window",
  "needsHumidity": true,
  "needsGrowLight": false,
  "coldDraftRisk": true
}
```

## Project Structure

```
├── Controllers/     # HTTP endpoints
├── Repositories/    # Data access layer
├── Models/          # Entities and DTOs
├── docker-compose.yml
└── Dockerfile
```

## Design Decisions

- **Controller-Repository pattern** keeps HTTP logic separate from data access, which makes the code easier to test and maintain.
- **Docker Compose** runs the API and database together, so it works with a single command.

## Roadmap

- [ ] `PUT` and `DELETE` endpoints
- [ ] Input validation and consistent error responses
- [ ] Unit and integration tests (xUnit)
- [ ] GitHub Actions CI
- [ ] Real outdoor temperature alerts via Environment Canada
- [ ] Simple web UI
- [ ] Public deployment

## Author

**Rossy**: [GitHub](https://github.com/rossyblue)