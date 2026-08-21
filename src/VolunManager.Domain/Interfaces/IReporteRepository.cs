using VolunManager.Domain.Entities;

namespace VolunManager.Domain.Interfaces
{
    /// <summary>
    /// Interfaz para el repositorio de reportes.
    /// Proporciona métodos para obtener datos agregados necesarios para generar reportes.
    /// </summary>
    public interface IReporteRepository
    {
        /// <summary>
        /// Obtiene todas las asistencias de un voluntario.
        /// </summary>
        Task<IEnumerable<Asistencia>> ObtenerAsistenciasVoluntarioAsync(int voluntarioId);

        /// <summary>
        /// Obtiene las asistencias de un voluntario en un rango de fechas.
        /// </summary>
        Task<IEnumerable<Asistencia>> ObtenerAsistenciasVoluntarioAsync(
            int voluntarioId,
            DateTime fechaInicio,
            DateTime fechaFin);

        /// <summary>
        /// Obtiene todas las asistencias de una jornada.
        /// </summary>
        Task<IEnumerable<Asistencia>> ObtenerAsistenciasJornadaAsync(int jornadaId);

        /// <summary>
        /// Obtiene las asistencias de una jornada en un rango de fechas.
        /// </summary>
        Task<IEnumerable<Asistencia>> ObtenerAsistenciasJornadaAsync(
            int jornadaId,
            DateTime fechaInicio,
            DateTime fechaFin);

        /// <summary>
        /// Obtiene un voluntario con sus datos completos (incluyendo rol).
        /// </summary>
        Task<Voluntario?> ObtenerVoluntarioAsync(int voluntarioId);

        /// <summary>
        /// Obtiene una jornada con sus datos completos.
        /// </summary>
        Task<Jornada?> ObtenerJornadaAsync(int jornadaId);

        /// <summary>
        /// Verifica si un voluntario existe.
        /// </summary>
        Task<bool> ExisteVoluntarioAsync(int voluntarioId);

        /// <summary>
        /// Verifica si una jornada existe.
        /// </summary>
        Task<bool> ExisteJornadaAsync(int jornadaId);
    }
}
