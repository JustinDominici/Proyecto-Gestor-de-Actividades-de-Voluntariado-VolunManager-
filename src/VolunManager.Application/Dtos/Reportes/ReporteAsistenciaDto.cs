namespace VolunManager.Application.Dtos.Reportes
{
    /// <summary>
    /// DTO para reportes de asistencia en una jornada.
    /// Contiene información de la jornada y los voluntarios que participaron.
    /// </summary>
    public class ReporteAsistenciaDto
    {
        public int JornadaId { get; set; }

        public string TituloJornada { get; set; } = string.Empty;

        public string DescripcionJornada { get; set; } = string.Empty;

        public DateTime FechaJornada { get; set; }

        public string LugarJornada { get; set; } = string.Empty;

        public int TotalVoluntariosParticipantes { get; set; }

        public double TotalHorasTrabajadas { get; set; }

        public double PromedioHorasPorVoluntario { get; set; }

        public DateTime? FechaInicio { get; set; }

        public DateTime? FechaFin { get; set; }

        public DateTime FechaReporte { get; set; } = DateTime.UtcNow;

        public List<DetalleAsistenciaDto> DetallesVoluntarios { get; set; } = new List<DetalleAsistenciaDto>();
    }

    /// <summary>
    /// Detalle de la asistencia de cada voluntario en una jornada.
    /// </summary>
    public class DetalleAsistenciaDto
    {
        public int VoluntarioId { get; set; }

        public string NombreVoluntario { get; set; } = string.Empty;

        public string ApellidoVoluntario { get; set; } = string.Empty;

        public string CorreoVoluntario { get; set; } = string.Empty;

        public DateTime HoraEntrada { get; set; }

        public DateTime? HoraSalida { get; set; }

        public double HorasTrabajadas { get; set; }
    }
}
