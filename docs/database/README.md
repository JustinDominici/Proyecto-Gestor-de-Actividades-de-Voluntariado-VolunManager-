\# Diseño de Base de Datos - VolunManager



Este documento describe el diseño inicial de la base de datos del sistema VolunManager.



\## Objetivo



Crear una estructura de datos que permita gestionar voluntarios, roles, jornadas, tareas, asistencias y reportes de horas.



\## Tablas principales



\### Roles

Almacena los roles que pueden tener los voluntarios dentro del sistema.



\### Voluntarios

Registra la información principal de cada voluntario, incluyendo nombre, correo, teléfono, rol y estado activo.



\### Jornadas

Representa las actividades o eventos de voluntariado programados.



\### Tareas

Permite asignar tareas específicas a voluntarios dentro de una jornada.



\### Asistencias

Registra la asistencia de los voluntarios a las jornadas, incluyendo hora de entrada, hora de salida y horas trabajadas.



\### ReportesHoras

Permite almacenar reportes de horas acumuladas por voluntario.



\## Relaciones principales



\- Un rol puede estar asignado a varios voluntarios.

\- Una jornada puede tener varias tareas.

\- Una tarea puede ser asignada a un voluntario.

\- Un voluntario puede registrar asistencia en varias jornadas.

\- Una jornada puede tener muchos registros de asistencia.

\- Un voluntario puede tener reportes de horas generados.



\## Issue relacionado



Este diseño corresponde al Issue #2: Diseñar base de datos del sistema.

