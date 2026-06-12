# Progress Tracker - VolunManager

## Objetivos Completados

### ✅ Issue #2: Diseñar base de datos del sistema
- [x] Crear tabla `voluntarios`
- [x] Crear tabla `jornadas`
- [x] Crear tabla `tareas`
- [x] Crear tabla `asistencias`
- [x] Definir claves primarias
- [x] Definir claves foráneas

### ✅ Issue #1: Crear modelo y CRUD de Voluntarios
- [x] Crear la clase `Voluntario`
- [x] Agregar atributos: `id`, `nombre`, `apellido`, `correo`, `telefono`, `rol`, `horas_acumuladas`
- [x] Crear función para registrar voluntarios
- [x] Crear función para listar voluntarios
- [x] Crear función para editar voluntarios
- [x] Crear función para eliminar voluntarios

### ✅ Issue #7: Crear modelo y CRUD de Voluntarios (Versión mejorada)
- [x] Modelo mejorado con validaciones y funciones básicas

### ✅ Issue #6: Crear gestión de Jornadas de Voluntariado
- [x] Crear la clase `Jornada`
- [x] Agregar atributos: `id`, `titulo`, `descripcion`, `fecha`, `lugar`, `estado`
- [x] Permitir crear nuevas jornadas
- [x] Permitir listar jornadas disponibles
- [x] Permitir actualizar una jornada

### ✅ Issue #5: Asignar tareas a voluntarios
- [x] Crear la clase `Tarea`
- [x] Agregar atributos: `id`, `nombre`, `descripcion`, `estado`, `id_jornada`, `id_voluntario`
- [x] Permitir asignar una tarea a un voluntario
- [x] Permitir asociar una tarea a una jornada

### ✅ Issue #4: Registrar asistencia de voluntarios
- [x] Crear la clase `Asistencia`
- [x] Agregar atributos: `id`, `id_voluntario`, `id_jornada`, `hora_entrada`, `hora_salida`, `horas_trabajadas`
- [x] Registrar hora de entrada y salida
- [x] Calcular automáticamente las horas trabajadas

### ✅ Issue #3: Crear reportes de horas y participación
- [x] Crear reporte de horas por voluntario
- [x] Crear reporte de asistencia por jornada
- [x] Mostrar total de horas acumuladas
- [x] Permitir filtrar reportes por fecha

---

## Próximos Pasos
- Implementar código en C# para cada módulo
- Conectar con base de datos
- Crear interfaz de usuario
