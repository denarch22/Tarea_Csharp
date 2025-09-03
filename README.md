# MyFirstApi (.NET 8 + PostgreSQL + EF Core)

Proyecto mínimo para un CRUD de `Todo` con Web API.

## Requisitos
- .NET 8 SDK
- PostgreSQL en `localhost:5432` (o ajusta la cadena en `appsettings.json`)
- dotnet-ef CLI (si no lo tienes): `dotnet tool install --global dotnet-ef`

## Pasos
```bash
dotnet restore
dotnet ef migrations add InitialCreate
dotnet ef database update
dotnet run
```

Swagger: abre `https://localhost:5001/swagger` (o el puerto que te muestre la consola).
