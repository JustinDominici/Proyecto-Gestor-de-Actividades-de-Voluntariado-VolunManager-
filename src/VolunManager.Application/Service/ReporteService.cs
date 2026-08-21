using VolunManager.Application.Contract;
using VolunManager.Application.Core;
using VolunManager.Application.Dtos.Reportes;
using VolunManager.Domain.Interfaces;

namespace VolunManager.Application.Service
{
    /// <summary>
    /// Servicio de reportes.
    /// Implementa dos versiones sobrecargadas de cada método (concepto POO: Sobrecarga).
    /// Demuestra polimorfismo al trabajar con la interfaz IReporteService.
    /// </summary>
    public class ReporteService : BaseService, IReporteService
    {
        private readonly IReporteRepository _reporteRepository;

        public ReporteService(IReporteRepository reporteRepository)
        {
            _reporteRepository = reporteRepository;
        }

        /// <summary>
        /// VERSIÓN 1: Genera reporte de horas SIN filtro de fechas.
        /// Incluye todas las asistencias del voluntario desde el inicio de los tiempos.
        /// </summary>
        public async Task<ServiceResult<ReporteHorasDto>> GenerarReporteHorasAsync(int voluntarioId)
        {
            // Validación básica
            if (voluntarioId <= 0)
            {
                return Fail<ReporteHorasDto>("El ID del voluntario no es válido.");
            }

            // Verificar que el voluntario existe
            var voluntarioExiste = await _reporteRepository.ExisteVoluntarioAsync(voluntarioId);
            if (!voluntarioExiste)
            {
                return NotFound<ReporteHorasDto>($"No existe un voluntario con el ID {voluntarioId}.");
            }

            // Obtener datos del voluntario
            var voluntario = await _reporteRepository.ObtenerVoluntarioAsync(voluntarioId);
            if (voluntario == null)
            {
                return NotFound<ReporteHorasDto>("Error al obtener los datos del voluntario.");
            }

            // Obtener TODAS las asistencias del voluntario (sin filtro de fechas)
            var asistencias = await _reporteRepository.ObtenerAsistenciasVoluntarioAsync(voluntarioId);

            // Construir el DTO del reporte
            var reporte = ConstruirReporteHoras(
                voluntario,
                asistencias.ToList(),
                fechaInicio: null,
                fechaFin: null
            );

            return Ok(reporte, "Reporte de horas generado correctamente.");
        }

        /// <summary>
        /// VERSIÓN 2 (SOBRECARGA): Genera reporte de horas CON filtro de fechas.
        /// Incluye solo asistencias en el rango de fechas especificado.
        /// 
        /// Demuestra SOBRECARGA de métodos (requisito POO):
        /// El mismo método GenerarReporteHorasAsync con diferentes parámetros.
        /// </summary>
        public async Task<ServiceResult<ReporteHorasDto>> GenerarReporteHorasAsync(
            int voluntarioId,
            DateTime fechaInicio,
            DateTime fechaFin)
        {
            // Validación básica
            if (voluntarioId <= 0)
            {
                return Fail<ReporteHorasDto>("El ID del voluntario no es válido.");
            }

            if (fechaInicio > fechaFin)
            {
                return Fail<ReporteHorasDto>("La fecha de inicio no puede ser posterior a la fecha de fin.");
            }

            // Verificar que el voluntario existe
            var voluntarioExiste = await _reporteRepository.ExisteVoluntarioAsync(voluntarioId);
            if (!voluntarioExiste)
            {
                return NotFound<ReporteHorasDto>($"No existe un voluntario con el ID {voluntarioId}.");
            }

            // Obtener datos del voluntario
            var voluntario = await _reporteRepository.ObtenerVoluntarioAsync(voluntarioId);
            if (voluntario == null)
            {
                return NotFound<ReporteHorasDto>("Error al obtener los datos del voluntario.");
            }

            // Obtener asistencias en el rango de fechas
            var asistencias = await _reporteRepository.ObtenerAsistenciasVoluntarioAsync(
                voluntarioId,
                fechaInicio,
                fechaFin
            );

            // Construir el DTO del reporte
            var reporte = ConstruirReporteHoras(
                voluntario,
                asistencias.ToList(),
                fechaInicio,
                fechaFin
            );

            return Ok(reporte, "Reporte de horas generado correctamente.");
        }

        /// <summary>
        /// VERSIÓN 1: Genera reporte de asistencia SIN filtro de fechas.
        /// Incluye todos los voluntarios que participaron en la jornada.
        /// </summary>
        public async Task<ServiceResult<ReporteAsistenciaDto>> GenerarReporteAsistenciaAsync(int jornadaId)
        {
            // Validación básica
            if (jornadaId <= 0)
            {
                return Fail<ReporteAsistenciaDto>("El ID de la jornada no es válido.");
            }

            // Verificar que la jornada existe
            var jornadaExiste = await _reporteRepository.ExisteJornadaAsync(jornadaId);
            if (!jornadaExiste)
            {
                return NotFound<ReporteAsistenciaDto>($"No existe una jornada con el ID {jornadaId}.");
            }

            // Obtener datos de la jornada
            var jornada = await _reporteRepository.ObtenerJornadaAsync(jornadaId);
            if (jornada == null)
            {
                return NotFound<ReporteAsistenciaDto>("Error al obtener los datos de la jornada.");
            }

            // Obtener TODAS las asistencias de la jornada
            var asistencias = await _reporteRepository.ObtenerAsistenciasJornadaAsync(jornadaId);

            // Construir el DTO del reporte
            var reporte = ConstruirReporteAsistencia(
                jornada,
                asistencias.ToList(),
                fechaInicio: null,
                fechaFin: null
            );

            return Ok(reporte, "Reporte de asistencia generado correctamente.");
        }

        /// <summary>
        /// VERSIÓN 2 (SOBRECARGA): Genera reporte de asistencia CON filtro de fechas.
        /// Incluye solo asistencias en el rango de fechas especificado.
        /// 
        /// Demuestra SOBRECARGA de métodos (requisito POO):
        /// El mismo método GenerarReporteAsistenciaAsync con diferentes parámetros.
        /// </summary>
        public async Task<ServiceResult<ReporteAsistenciaDto>> GenerarReporteAsistenciaAsync(
            int jornadaId,
            DateTime fechaInicio,
            DateTime fechaFin)
        {
            // Validación básica
            if (jornadaId <= 0)
            {
                return Fail<ReporteAsistenciaDto>("El ID de la jornada no es válido.");
            }

            if (fechaInicio > fechaFin)
            {
                return Fail<ReporteAsistenciaDto>("La fecha de inicio no puede ser posterior a la fecha de fin.");
            }

            // Verificar que la jornada existe
            var jornadaExiste = await _reporteRepository.ExisteJornadaAsync(jornadaId);
            if (!jornadaExiste)
            {
                return NotFound<ReporteAsistenciaDto>($"No existe una jornada con el ID {jornadaId}.");
            }

            // Obtener datos de la jornada
            var jornada = await _reporteRepository.ObtenerJornadaAsync(jornadaId);
            if (jornada == null)
            {
                return NotFound<ReporteAsistenciaDto>("Error al obtener los datos de la jornada.");
            }

            // Obtener asistencias en el rango de fechas
            var asistencias = await _reporteRepository.ObtenerAsistenciasJornadaAsync(
                jornadaId,
                fechaInicio,
                fechaFin
            );

            // Construir el DTO del reporte
            var reporte = ConstruirReporteAsistencia(
                jornada,
                asistencias.ToList(),
                fechaInicio,
                fechaFin
            );

            return Ok(reporte, "Reporte de asistencia generado correctamente.");
        }

        /// <summary>
        /// Método privado para construir el reporte de horas.
        /// Encapsula la lógica de cálculo de estadísticas.
        /// </summary>
        private static ReporteHorasDto ConstruirReporteHoras(
            Domain.Entities.Voluntario voluntario,
            List<Domain.Entities.Asistencia> asistencias,
            DateTime? fechaInicio,
            DateTime? fechaFin)
        {
            var totalHoras = asistencias.Sum(a => a.HorasTrabajadas);
            var cantidadJornadas = asistencias.Count;
            var promedioHoras = cantidadJornadas > 0 ? totalHoras / cantidadJornadas : 0;

            var detalles = asistencias
                .Select(a => new DetalleHorasDto
                {
                    JornadaId = a.JornadaId,
                    TituloJornada = a.Jornada?.Titulo ?? "Sin título",
                    FechaJornada = a.Jornada?.Fecha ?? DateTime.MinValue,
                    HorasTrabajadas = a.HorasTrabajadas
                })
                .ToList();

            return new ReporteHorasDto
            {
                VoluntarioId = voluntario.Id,
                NombreVoluntario = voluntario.Nombre,
                ApellidoVoluntario = voluntario.Apellido,
                CorreoVoluntario = voluntario.Correo,
                TotalHorasTrabajadas = Math.Round(totalHoras, 2),
                CantidadJornadasRealizadas = cantidadJornadas,
                PromedioHorasPorJornada = Math.Round(promedioHoras, 2),
                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
                FechaReporte = DateTime.UtcNow,
                DetallesJornadas = detalles
            };
        }

        /// <summary>
        /// Método privado para construir el reporte de asistencia.
        /// Encapsula la lógica de cálculo de estadísticas.
        /// </summary>
        private static ReporteAsistenciaDto ConstruirReporteAsistencia(
            Domain.Entities.Jornada jornada,
            List<Domain.Entities.Asistencia> asistencias,
            DateTime? fechaInicio,
            DateTime? fechaFin)
        {
            var totalHoras = asistencias.Sum(a => a.HorasTrabajadas);
            var cantidadVoluntarios = asistencias.Count;
            var promedioHoras = cantidadVoluntarios > 0 ? totalHoras / cantidadVoluntarios : 0;

            var detalles = asistencias
                .Select(a => new DetalleAsistenciaDto
                {
                    VoluntarioId = a.VoluntarioId,
                    NombreVoluntario = a.Voluntario?.Nombre ?? "Sin nombre",
                    ApellidoVoluntario = a.Voluntario?.Apellido ?? "Sin apellido",
                    CorreoVoluntario = a.Voluntario?.Correo ?? "Sin correo",
                    HoraEntrada = a.HoraEntrada,
                    HoraSalida = a.HoraSalida,
                    HorasTrabajadas = a.HorasTrabajadas
                })
                .ToList();

            return new ReporteAsistenciaDto
            {
                JornadaId = jornada.Id,
                TituloJornada = jornada.Titulo,
                DescripcionJornada = jornada.Descripcion,
                FechaJornada = jornada.Fecha,
                LugarJornada = jornada.Lugar,
                TotalVoluntariosParticipantes = cantidadVoluntarios,
                TotalHorasTrabajadas = Math.Round(totalHoras, 2),
                PromedioHorasPorVoluntario = Math.Round(promedioHoras, 2),
                FechaInicio = fechaInicio,
                FechaFin = fechaFin,
                FechaReporte = DateTime.UtcNow,
                DetallesVoluntarios = detalles
            };
        }
    }
}
