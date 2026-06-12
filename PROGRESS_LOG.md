# 📋 Histórico de Progreso - VolunManager

## Resumen del Proyecto
Sistema gestor de actividades de voluntariado que permite administrar voluntarios, jornadas, tareas, asistencias y generar reportes.

---

## 🎯 Issues Creados

### Issue #1: Crear modelo y CRUD de Voluntarios
**Estado:** 🔴 Open | **Asignado a:** @JustinDominici  
**Creado hace:** 46 minutos

**Descripción:**
Implementar la clase/modelo `Voluntario` y las funciones básicas para registrar, consultar, editar y eliminar voluntarios.

**Tareas:**
- [ ] Crear la clase `Voluntario`
- [ ] Agregar atributos: `id`, `nombre`, `apellido`, `correo`, `telefono`, `rol`, `horas_acumuladas`
- [ ] Crear función para registrar voluntarios
- [ ] Crear función para listar voluntarios
- [ ] Crear función para editar voluntarios
- [ ] Crear función para eliminar voluntarios
- [ ] Conectar el CRUD con la base de datos

**Criterios de aceptación:**
- El sistema permite registrar voluntarios correctamente
- No acepta campos importantes vacíos
- Los datos quedan guardados en la base de datos

---

### Issue #2: Diseñar base de datos del sistema
**Estado:** 🟡 Open (Prerequisito)  
**Creado hace:** 33 minutos

**Descripción:**
Crear la estructura inicial de la base de datos para almacenar voluntarios, jornadas, tareas y asistencias.

**Tareas:**
- [ ] Crear tabla `voluntarios`
- [ ] Crear tabla `jornadas`
- [ ] Crear tabla `tareas`
- [ ] Crear tabla `asistencias`
- [ ] Definir claves primarias
- [ ] Definir claves foráneas
- [ ] Relacionar las tablas correctamente

**Criterios de aceptación:**
- La base de datos permite guardar toda la información principal del sistema
- Las relaciones entre tablas funcionan correctamente
- No debe existir asistencia sin voluntario ni jornada asociada

---

### Issue #3: Crear reportes de horas y participación
**Estado:** 🔴 Open  
**Creado hace:** 33 minutos

**Descripción:**
Agregar reportes para visualizar las horas acumuladas y la participación de los voluntarios.

**Tareas:**
- [ ] Crear reporte de horas por voluntario
- [ ] Crear reporte de asistencia por jornada
- [ ] Mostrar total de horas acumuladas
- [ ] Permitir filtrar reportes por fecha
- [ ] Permitir filtrar reportes por jornada

**Criterios de aceptación:**
- El sistema muestra las horas acumuladas por voluntario
- El sistema permite consultar la participación en cada jornada
- Los reportes obtienen los datos desde la base de datos

---

### Issue #4: Registrar asistencia de voluntarios
**Estado:** 🔴 Open  
**Creado hace:** 32 minutos

**Descripción:**
Implementar la clase/modelo `Asistencia` para registrar la participación de los voluntarios en cada jornada.

**Tareas:**
- [ ] Crear la clase `Asistencia`
- [ ] Agregar atributos: `id`, `id_voluntario`, `id_jornada`, `hora_entrada`, `hora_salida`, `horas_trabajadas`
- [ ] Registrar hora de entrada
- [ ] Registrar hora de salida
- [ ] Calcular automáticamente las horas trabajadas
- [ ] Guardar la asistencia en la base de datos

**Criterios de aceptación:**
- El sistema permite registrar asistencia por jornada
- Las horas trabajadas se calculan correctamente
- La asistencia queda guardada en la base de datos

---

### Issue #5: Asignar tareas a voluntarios
**Estado:** 🔴 Open  
**Creado hace:** 32 minutos

**Descripción:**
Crear la funcionalidad para asignar tareas específicas a los voluntarios dentro de una jornada.

**Tareas:**
- [ ] Crear la clase `Tarea`
- [ ] Agregar atributos: `id`, `nombre`, `descripcion`, `estado`, `id_jornada`, `id_voluntario`
- [ ] Permitir asignar una tarea a un voluntario
- [ ] Permitir asociar una tarea a una jornada
- [ ] Permitir cambiar estado: pendiente, en proceso y completada

**Criterios de aceptación:**
- Una tarea debe estar asociada a una jornada
- Una tarea puede ser asignada a un voluntario
- El sistema permite actualizar el estado de la tarea

---

### Issue #6: Crear gestión de Jornadas de Voluntariado
**Estado:** 🔴 Open  
**Creado hace:** 32 minutos

**Descripción:**
Implementar la clase/modelo `Jornada` para administrar las actividades o eventos de voluntariado.

**Tareas:**
- [ ] Crear la clase `Jornada`
- [ ] Agregar atributos: `id`, `titulo`, `descripcion`, `fecha`, `lugar`, `estado`
- [ ] Permitir crear nuevas jornadas
- [ ] Permitir listar jornadas disponibles
- [ ] Permitir actualizar una jornada
- [ ] Permitir cancelar una jornada

**Criterios de aceptación:**
- Se pueden crear jornadas desde el sistema
- Cada jornada tiene título, fecha y lugar
- El sistema muestra las jornadas registradas

---

### Issue #7: Crear modelo y CRUD de Voluntarios
**Estado:** 🔴 Open  
**Creado hace:** 32 minutos

**Descripción:**
Implementar la clase/modelo `Voluntario` y las funciones básicas para registrar, consultar, editar y eliminar voluntarios.

**Tareas:**
- [ ] Crear la clase `Voluntario`
- [ ] Agregar atributos: `id`, `nombre`, `apellido`, `correo`, `telefono`, `rol`, `horas_acumuladas`
- [ ] Crear función para registrar voluntarios
- [ ] Crear función para listar voluntarios
- [ ] Crear función para editar voluntarios
- [ ] Crear función para eliminar voluntarios
- [ ] Conectar el CRUD con la base de datos

**Criterios de aceptación:**
- El sistema permite registrar voluntarios correctamente
- No acepta campos importantes vacíos
- Los datos quedan guardados en la base de datos

---

## 🔄 Flujo de Trabajo Recomendado

### Para mover un issue a "In Progress":
1. Abre el issue en GitHub
2. En la sección de **Projects**, selecciona el proyecto
3. Cambia el estado de "Backlog" a "In Progress"
4. Asigna el issue a ti mismo
5. Agrega comentarios describiendo qué estás haciendo

### Para cambiar el estado de una tarea:
1. Dentro del issue, actualiza los checkboxes `- [ ]` a `- [x]`
2. Comenta en el issue describiendo el progreso
3. Cuando termines todas las tareas, cierra el issue

### Prioridad de Desarrollo:
1. **Issue #2** - Diseñar BD (debe hacerse primero)
2. **Issue #1** - CRUD Voluntarios (depende de BD)
3. **Issue #6** - CRUD Jornadas
4. **Issue #4** - Asistencia
5. **Issue #5** - Tareas
6. **Issue #3** - Reportes

---

## 📊 Estado del Proyecto

| Issue | Título | Estado | Progreso |
|-------|--------|--------|----------|
| #1 | CRUD Voluntarios | 🔴 Open | 0% |
| #2 | Base de Datos | 🔴 Open | 0% |
| #3 | Reportes | 🔴 Open | 0% |
| #4 | Asistencia | 🔴 Open | 0% |
| #5 | Tareas | 🔴 Open | 0% |
| #6 | Jornadas | 🔴 Open | 0% |
| #7 | CRUD Voluntarios (v2) | 🔴 Open | 0% |

**Progreso General:** 0/7 Issues Completados

---

## 📝 Actualizaciones Recientes

- ✅ Creados 7 issues iniciales del proyecto
- 📋 Documentado el histórico de progreso
- 🎯 Definida la prioridad de desarrollo

---

## 🚀 Próximos Pasos

1. Abrir **Issue #2** y marcar como "In Progress"
2. Diseñar el diagrama ER de la base de datos
3. Crear los scripts SQL para las tablas
4. Conectar la aplicación con la BD
5. Implementar el CRUD de Voluntarios

---

**Última actualización:** Junio 12, 2026  
**Rama:** Develop  
**Versión:** 0.1.0 (Pre-release)
