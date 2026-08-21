using Microsoft.AspNetCore.Mvc;
using VolunManager.Api.Extensions;
using VolunManager.Application.Contract;

namespace VolunManager.Api.Controllers
{
    /// <summary>
    /// Controlador para la generación de reportes.
    /// Expone endpoints para obtener reportes de horas y asistencia.
    /// Demuestra sobrecarga de métodos a nivel de API (dos versiones de cada endpoint).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly IReporteService _reporteService;

        public ReportesController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        /// <summary>
        /// Obtiene el reporte de horas trabajadas de un voluntario (sin filtro de fechas).
        /// Incluye todas las asistencias registradas.
        /// </summary>
        /// <param name="voluntarioId">ID del voluntario</param>
        /// <returns>Reporte de horas del voluntario</returns>
        [HttpGet("horas/{voluntarioId}")]
        public async Task<IActionResult> ObtenerReporteHoras(int voluntarioId)
        {
            var result = await _reporteService.GenerarReporteHorasAsync(voluntarioId);

            return result.ToActionResult();
        }

        /// <summary>
        /// Obtiene el reporte de horas trabajadas de un voluntario (con filtro de fechas).
        /// SOBRECARGA: Versión con parámetros de fecha.
        /// Demuestra el concepto POO de sobrecarga a nivel de API.
        /// </summary>
        /// <param name="voluntarioId">ID del voluntario</param>
        /// <param name="fechaInicio">Fecha de inicio del rango (YYYY-MM-DD)</param>
        /// <param name="fechaFin">Fecha de fin del rango (YYYY-MM-DD)</param>
        /// <returns>Reporte de horas del voluntario en el rango de fechas</returns>
        [HttpGet("horas/{voluntarioId}/rango")]
        public async Task<IActionResult> ObtenerReporteHorasRango(
            int voluntarioId,
            [FromQuery] DateTime fechaInicio,
            [FromQuery] DateTime fechaFin)
        {
            var result = await _reporteService.GenerarReporteHorasAsync(voluntarioId, fechaInicio, fechaFin);

            return result.ToActionResult();
        }

        /// <summary>
        /// Obtiene el reporte de asistencia de una jornada (sin filtro de fechas).
        /// Incluye todos los voluntarios que participaron.
        /// </summary>
        /// <param name="jornadaId">ID de la jornada</param>
        /// <returns>Reporte de asistencia de la jornada</returns>
        [HttpGet("asistencia/{jornadaId}")]
        public async Task<IActionResult> ObtenerReporteAsistencia(int jornadaId)
        {
            var result = await _reporteService.GenerarReporteAsistenciaAsync(jornadaId);

            return result.ToActionResult();
        }

        /// <summary>
        /// Obtiene el reporte de asistencia de una jornada (con filtro de fechas).
        /// SOBRECARGA: Versión con parámetros de fecha.
        /// Demuestra el concepto POO de sobrecarga a nivel de API.
        /// </summary>
        /// <param name="jornadaId">ID de la jornada</param>
        /// <param name="fechaInicio">Fecha de inicio del rango (YYYY-MM-DD)</param>
        /// <param name="fechaFin">Fecha de fin del rango (YYYY-MM-DD)</param>
        /// <returns>Reporte de asistencia de la jornada en el rango de fechas</returns>
        [HttpGet("asistencia/{jornadaId}/rango")]
        public async Task<IActionResult> ObtenerReporteAsistenciaRango(
            int jornadaId,
            [FromQuery] DateTime fechaInicio,
            [FromQuery] DateTime fechaFin)
        {
            var result = await _reporteService.GenerarReporteAsistenciaAsync(jornadaId, fechaInicio, fechaFin);

            return result.ToActionResult();
        }
    }
}
