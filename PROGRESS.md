# Progress Tracker - VolunManager

## Objetivos Completados

### 🟢 Issue #3: Crear reportes de horas y participación
- [x] Crear reporte de horas por voluntario
- [x] Crear reporte de asistencia por jornada
- [x] Mostrar total de horas acumuladas
- [x] Permitir filtrar reportes por fecha
**Estado:** Completado - Implementado con sobrecarga en ReporteService y UI en Blazor.

### 🟢 Issue #4: Registrar asistencia de voluntarios
- [x] Crear la clase `Asistencia`
- [x] Agregar atributos: `id`, `id_voluntario`, `id_jornada`, `hora_entrada`, `hora_salida`, `horas_trabajadas`
- [x] Registrar hora de entrada y salida
- [x] Calcular automáticamente las horas trabajadas
**Estado:** Completado - Integridad referencial configurada en BD y endpoints CRUD funcionales.

### 🟢 Issue #5: Asignar tareas a voluntarios
- [x] Crear la clase `Tarea`
- [x] Agregar atributos: `id`, `titulo`, `descripcion`, `estado`, `id_jornada`, `id_voluntario`
- [x] Permitir asignar una tarea a un voluntario
- [x] Permitir asociar una tarea a una jornada
**Estado:** Completado - Restricciones de borrado en cascada (Cascade/Restrict) aplicadas correctamente.

### 🟢 Issue #6: Crear gestión de Jornadas de Voluntariado
- [x] Crear la clase `Jornada`
- [x] Agregar atributos: `id`, `titulo`, `descripcion`, `fecha`, `lugar`, `estado`
- [x] Permitir crear nuevas jornadas
- [x] Permitir listar jornadas disponibles
- [x] Permitir actualizar una jornada
**Estado:** Completado - CRUD completo expuesto en la API y consumido por Blazor WebAssembly.

---
## Hitos Finales
- [x] Implementar código en C# para cada módulo
- [x] Conectar con base de datos (SQL Server LocalDB / Fluent API)
- [x] Crear interfaz de usuario distribuida (Blazor WebAssembly)
- [x] Documentación final (README, diagramas Mermaid, esquemas)