# Documento de Requerimientos — VolunManager

## Problema
Las organizaciones de voluntariado necesitan coordinar participantes, jornadas, tareas y horas. Mantener estos datos en hojas de cálculo y registros separados dificulta el seguimiento y los reportes.

## Objetivo
Desarrollar un sistema que centralice voluntarios, roles, jornadas, tareas, asistencias y reportes mediante una API REST conectada a SQL Server y un cliente independiente.

## Actores
- Administrador: gestiona el sistema.
- Coordinador: organiza jornadas y supervisa voluntarios.
- Voluntario: participa en jornadas y ejecuta tareas.

## Requerimientos funcionales
- RF01: CRUD de roles.
- RF02: CRUD de voluntarios.
- RF03: CRUD de jornadas.
- RF04: CRUD de tareas.
- RF05: CRUD de asistencias.
- RF06: calcular horas trabajadas automáticamente.
- RF07: generar reporte de horas por voluntario.
- RF08: generar reporte de asistencia por jornada.
- RF09: filtrar reportes por rango de fechas.
- RF10: consumir la API desde un cliente independiente mediante HTTP/JSON.

## Requerimientos no funcionales
- ASP.NET Core Web API.
- SQL Server + Entity Framework Core.
- Separación de proyectos/capas.
- Respuestas HTTP consistentes.
- Manejo global de excepciones.
- Cliente distribuido.

## Reglas de negocio
- El correo del voluntario debe ser válido y único.
- El voluntario debe tener un rol existente.
- Una tarea debe referenciar jornada y voluntario existentes.
- La salida de asistencia debe ser posterior a la entrada.
- Un voluntario no puede registrar dos asistencias en la misma jornada.
- No se puede eliminar un rol con voluntarios asociados.

## Entidades
Rol, Voluntario, Jornada, Tarea y Asistencia.

## POO
El proyecto demuestra clases normales, constructores, encapsulamiento, abstracción, herencia, polimorfismo, métodos abstractos y sobrecarga.

## Criterios de aceptación
La API debe ejecutarse, conectarse a la BD, exponer los módulos en Swagger, generar reportes y ser consumible por el cliente Blazor.
