# Progress Tracker - VolunManager

## Objetivos Completados

### 🟡 Issue #3: Crear reportes de horas y participación
- [ ] Crear reporte de horas por voluntario
- [ ] Crear reporte de asistencia por jornada
- [ ] Mostrar total de horas acumuladas
- [ ] Permitir filtrar reportes por fecha

**Estado:** En Progreso - Diseño de estructura de reportes

### 🟡 Issue #4: Registrar asistencia de voluntarios
- [ ] Crear la clase `Asistencia`
- [ ] Agregar atributos: `id`, `id_voluntario`, `id_jornada`, `hora_entrada`, `hora_salida`, `horas_trabajadas`
- [ ] Registrar hora de entrada y salida
- [ ] Calcular automáticamente las horas trabajadas

**Estado:** En Progreso - Análisis de requerimientos

### 🟡 Issue #5: Asignar tareas a voluntarios
- [ ] Crear la clase `Tarea`
- [ ] Agregar atributos: `id`, `nombre`, `descripcion`, `estado`, `id_jornada`, `id_voluntario`
- [ ] Permitir asignar una tarea a un voluntario
- [ ] Permitir asociar una tarea a una jornada

**Estado:** En Progreso - Diseño de modelo

### 🟡 Issue #6: Crear gestión de Jornadas de Voluntariado
- [ ] Crear la clase `Jornada`
- [ ] Agregar atributos: `id`, `titulo`, `descripcion`, `fecha`, `lugar`, `estado`
- [ ] Permitir crear nuevas jornadas
- [ ] Permitir listar jornadas disponibles
- [ ] Permitir actualizar una jornada

**Estado:** En Progreso - Especificaciones

---

## Próximos Pasos
- Implementar código en C# para cada módulo
- Conectar con base de datos
- Crear interfaz de usuario
