namespace VolunManager.Application.Dtos.Reportes
{
    /// <summary>
    /// DTO para reportes de horas acumuladas de un voluntario.
    /// Contiene información del voluntario y estadísticas de horas trabajadas.
    /// </summary>
    public class ReporteHorasDto
    {
        public int VoluntarioId { get; set; }

        public string NombreVoluntario { get; set; } = string.Empty;

        public string ApellidoVoluntario { get; set; } = string.Empty;

        public string CorreoVoluntario { get; set; } = string.Empty;

        public double TotalHorasTrabajadas { get; set; }

        public int CantidadJornadasRealizadas { get; set; }

        public double PromedioHorasPorJornada { get; set; }

        /// <summary>
        /// Null si no se especificó rango de fechas en el reporte.
        /// </summary>
        public DateTime? FechaInicio { get; set; }

        /// <summary>
        /// Null si no se especificó rango de fechas en el reporte.
        /// </summary>
        public DateTime? FechaFin { get; set; }

        public DateTime FechaReporte { get; set; } = DateTime.UtcNow;

        public List<DetalleHorasDto> DetallesJornadas { get; set; } = new List<DetalleHorasDto>();
    }

    /// <summary>
    /// Detalle de las horas trabajadas en cada jornada.
    /// </summary>
    public class DetalleHorasDto
    {
        public int JornadaId { get; set; }

        public string TituloJornada { get; set; } = string.Empty;

        public DateTime FechaJornada { get; set; }

        public double HorasTrabajadas { get; set; }
    }
}
