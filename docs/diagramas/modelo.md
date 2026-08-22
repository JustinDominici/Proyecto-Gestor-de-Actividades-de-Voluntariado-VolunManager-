# Diagramas del sistema

## Clases

```mermaid
classDiagram
    class BaseEntity {
        <<abstract>>
        +int Id
        +ObtenerResumen() string
    }
    class Rol
    class Voluntario
    class Jornada
    class Tarea
    class Asistencia
    BaseEntity <|-- Rol
    BaseEntity <|-- Voluntario
    BaseEntity <|-- Jornada
    BaseEntity <|-- Tarea
    BaseEntity <|-- Asistencia
    Rol "1" --> "0..*" Voluntario
    Voluntario "1" --> "0..*" Jornada
    Jornada "1" --> "0..*" Tarea
    Voluntario "1" --> "0..*" Tarea
    Jornada "1" --> "0..*" Asistencia
    Voluntario "1" --> "0..*" Asistencia
```

## Base de datos

```mermaid
erDiagram
    ROLES ||--o{ VOLUNTARIOS : asigna
    VOLUNTARIOS ||--o{ JORNADAS : participa
    JORNADAS ||--o{ TAREAS : contiene
    VOLUNTARIOS ||--o{ TAREAS : recibe
    JORNADAS ||--o{ ASISTENCIAS : registra
    VOLUNTARIOS ||--o{ ASISTENCIAS : registra
```

## POO

- Abstracción: `BaseEntity`.
- Herencia: las entidades concretas derivan de `BaseEntity`.
- Polimorfismo: cada entidad implementa `ObtenerResumen()`.
- Encapsulamiento: setters privados y métodos de dominio.
- Constructores: parametrizados y protegidos para EF Core.
- Sobrecarga: `ReporteService` tiene métodos con y sin rango de fechas.
