# Pokemon API

API REST desarrollada con ASP.NET Core (.NET 10), Entity Framework Core y PostgreSQL. Incluye autenticación con JWT (ASP.NET Identity) y documentación interactiva con Scalar.

## Tecnologías

- ASP.NET Core 10
- Entity Framework Core
- PostgreSQL (Neon)
- JWT Authentication (ASP.NET Identity)
- Scalar (documentación OpenAPI)
- Docker

## Endpoints principales

- `POST /api/auth/register` - Registro de usuario
- `POST /api/auth/login` - Login (devuelve JWT)
- `GET /api/pokemon` - Listar pokémon (requiere auth)
- `POST /api/pokemon` - Crear pokémon (requiere auth)
- `PUT /api/pokemon/{id}` - Actualizar pokémon (requiere auth)
- `DELETE /api/pokemon/{id}` - Eliminar pokémon (requiere auth)

## Ejecutar localmente

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "TU_CONNECTION_STRING"
dotnet run
```

La documentación interactiva está disponible en `/scalar/v1`.