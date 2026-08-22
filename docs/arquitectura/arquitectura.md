# Arquitectura de VolunManager

## Arquitectura distribuida

```mermaid
flowchart LR
    C[VolunManager.Client<br/>Blazor WebAssembly]
    A[VolunManager.Api<br/>ASP.NET Core]
    AP[VolunManager.Application]
    D[VolunManager.Domain]
    I[VolunManager.Infrastructure]
    DB[(SQL Server)]
    C -->|HTTP/JSON| A
    A --> AP
    AP --> D
    AP --> I
    I --> D
    I --> DB
```

## Flujo

```text
Usuario -> Blazor Client -> HTTP/JSON -> API -> Service -> Repository -> EF Core -> SQL Server
```

## Responsabilidades

- Client: interfaz independiente.
- Api: Controllers, Swagger, CORS y manejo global de errores.
- Application: servicios, validaciones y DTOs.
- Domain: entidades y abstracciones.
- Infrastructure: repositorios, EF Core y migraciones.

El objetivo de esta separación es permitir que distintos clientes consuman la misma API sin acoplarse al acceso a datos.
