# Pokemon API

API REST desarrollada con ASP.NET Core (.NET 10), Entity Framework Core y PostgreSQL. Incluye autenticación con JWT (ASP.NET Identity) y documentación interactiva con Scalar.

## Tecnologías

- ASP.NET Core 10
- Entity Framework Core
- PostgreSQL
- JWT Authentication (ASP.NET Identity)
- Scalar (documentación OpenAPI)
- xUnit (tests unitarios)
- Docker + Docker Compose

## Pruébala en vivo

La API está desplegada en Render. Puedes explorar y probar todos los endpoints desde la documentación interactiva:

**[https://pokemonapi-mavn.onrender.com/scalar/](https://pokemonapi-mavn.onrender.com/scalar/)**

## Endpoints

| Método | Ruta | Auth | Descripción |
|--------|------|------|-------------|
| POST | `/api/auth/register` | No | Registro de usuario |
| POST | `/api/auth/login` | No | Login, devuelve JWT |
| GET | `/api/pokemon?page=1&pageSize=10` | Sí | Listar pokémon paginado |
| GET | `/api/pokemon/{id}` | Sí | Obtener pokémon por id |
| POST | `/api/pokemon` | Sí | Crear pokémon |
| PUT | `/api/pokemon/{id}` | Sí | Actualizar pokémon |
| DELETE | `/api/pokemon/{id}` | Sí | Eliminar pokémon |

## Arquitectura

El proyecto separa responsabilidades en capas:

- **Controllers** — reciben la request y devuelven la response, sin lógica de negocio
- **Services** — contienen la lógica de negocio e interactúan con la base de datos
- **DTOs** — definen qué datos entran y salen de la API, sin exponer las entidades directamente

## Ejecutar con Docker

```bash
docker-compose up --build
```

La API queda disponible en `http://localhost:8080` y la documentación interactiva en `http://localhost:8080/scalar/v1`.

## Ejecutar localmente

```bash
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "TU_CONNECTION_STRING"
dotnet run
```

La documentación interactiva queda disponible en `http://localhost:5000/scalar/v1`.

## Tests

```bash
cd PokemonApi.Tests
dotnet test
```