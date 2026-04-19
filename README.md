# CozyGen API

A production-style `RESTful API` built with `ASP.NET Core` and `C#` on `.NET 9`.
The system is designed with layered architecture, asynchronous processing, centralized error handling, and structured logging.

## Overview

This project exposes API endpoints for core e-commerce flows such as users, products, categories, styles, and orders.
It follows REST principles and is structured for maintainability, scalability, and testability.

## Architecture

The solution is organized into multiple projects with clear responsibility boundaries:

- `Api` – Controllers, middleware, application startup
- `Services` – Business logic and orchestration
- `repository` – Data access and persistence
- `Dto` – DTO contracts for API/service communication
- `Model` – Domain/entity models
- `Tests` – Automated unit and integration tests

### Dependency Injection & Decoupling

Communication between layers is handled through `Dependency Injection` and interfaces.
This reduces coupling, improves testability, and supports clean separation of concerns.

## Data Access

- ORM: `Entity Framework Core`
- Approach: `Database First`
- DB access operations are implemented asynchronously where relevant (`async/await`) to improve throughput and scalability.

## Mapping & DTOs

The API uses a dedicated DTO layer to isolate external contracts from persistence models.
Entity-to-DTO conversion is handled through `AutoMapper`.

## Configuration Management

Environment and runtime settings are externalized via `appsettings` files (for example, connection strings and environment-specific values), keeping configuration separate from source code.

## Logging, Monitoring, and Error Handling

- Logging framework: `NLog`
- Centralized exception handling: `ErrorHandlingMiddleware`
- Request traffic capture: `RatingMiddleware`, persisted in the `RATING` table for monitoring/analytics

## Testing Strategy

The `Tests` project includes:

- `Unit Tests` – validate isolated business and repository behaviors
- `Integration Tests` – validate end-to-end data access and component integration

## Technology Stack

- `.NET 9`
- `ASP.NET Core Web API`
- `C#`
- `Entity Framework Core`
- `AutoMapper`
- `NLog`
- `xUnit`

## Run the Project

1. Configure connection strings and required settings in `appsettings.json` / environment-specific files.
2. Build the solution.
3. Run the `Api` project (`project1`).
4. Use Swagger/OpenAPI in development to test endpoints.

---

