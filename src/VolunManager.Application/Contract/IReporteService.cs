using VolunManager.Application.Core;
using VolunManager.Application.Dtos.Reportes;

namespace VolunManager.Application.Contract
{
    /// <summary>
    /// Interfaz para el servicio de reportes.
    /// Proporciona dos versiones sobrecargadas de cada método de reporte,
    /// demostrando polimorfismo y sobrecarga (requisitos POO).
    /// </summary>
    public interface IReporteService
    {
        /// <summary>
        /// Genera un reporte completo de horas trabajadas por un voluntario.
        /// Incluye todas las asistencias registradas sin filtro de fechas.
        /// 
        /// VERSIÓN 1: Sin filtro de fechas (todas las horas acumuladas).
        /// </summary>
        Task<ServiceResult<ReporteHorasDto>> GenerarReporteHorasAsync(int voluntarioId);

        /// <summary>
        /// Genera un reporte de horas trabajadas por un voluntario en un rango de fechas.
        /// 
        /// VERSIÓN 2 (SOBRECARGA): Con filtro de fechas inicio y fin.
        /// Demuestra sobrecarga de métodos (concepto POO).
        /// </summary>
        Task<ServiceResult<ReporteHorasDto>> GenerarReporteHorasAsync(
            int voluntarioId,
            DateTime fechaInicio,
            DateTime fechaFin);

        /// <summary>
        /// Genera un reporte de asistencia completo de una jornada.
        /// Incluye todos los voluntarios que participaron sin filtro de fechas.
        /// 
        /// VERSIÓN 1: Sin filtro de fechas.
        /// </summary>
        Task<ServiceResult<ReporteAsistenciaDto>> GenerarReporteAsistenciaAsync(int jornadaId);

        /// <summary>
        /// Genera un reporte de asistencia de una jornada en un rango de fechas.
        /// 
        /// VERSIÓN 2 (SOBRECARGA): Con filtro de fechas.
        /// Demuestra sobrecarga de métodos (concepto POO).
        /// </summary>
        Task<ServiceResult<ReporteAsistenciaDto>> GenerarReporteAsistenciaAsync(
            int jornadaId,
            DateTime fechaInicio,
            DateTime fechaFin);
    }
}
