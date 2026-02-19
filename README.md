# Internet_jysk

---- Small ASP.NET Core Web API project for internet shop practice.

It has 3 projects:
- `Domain` (entities, dto, response models)
- `Infrastructure` (services, interfaces, db context)
- `WebJysk` (API controllers, Program.cs)

## What is inside

- JWT auth (register/login)
- User, Product, Order, Category, Cart controllers
- Email service (test email, forgot/reset password)
- Swagger UI

## Requirements

- .NET SDK 9.0
- PostgreSQL

## Quick start

* Go to API project:
* cd WebJysk

* Run app:
* by:
* dotnet run

## Notes

- Some endpoints use `[Authorize]`, so get JWT token first from auth endpoints.
- Roles are seeded on app startup.
- This project is for learning and can be improved.

