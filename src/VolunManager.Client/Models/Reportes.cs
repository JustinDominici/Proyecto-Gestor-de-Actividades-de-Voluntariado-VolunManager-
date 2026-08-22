namespace VolunManager.Client.Models;

public class ReporteHorasDto
{
    public int VoluntarioId { get; set; }
    public string NombreVoluntario { get; set; } = string.Empty;
    public string ApellidoVoluntario { get; set; } = string.Empty;
    public string CorreoVoluntario { get; set; } = string.Empty;
    public double TotalHorasTrabajadas { get; set; }
    public int CantidadJornadasRealizadas { get; set; }
    public double PromedioHorasPorJornada { get; set; }
    public DateTime? FechaInicio { get; set; }
    public DateTime? FechaFin { get; set; }
    public DateTime FechaReporte { get; set; }
    public List<DetalleHorasDto> DetallesJornadas { get; set; } = [];
}
public class DetalleHorasDto
{
    public int JornadaId { get; set; }
    public string TituloJornada { get; set; } = string.Empty;
    public DateTime FechaJornada { get; set; }
    public double HorasTrabajadas { get; set; }
}
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
    public DateTime FechaReporte { get; set; }
    public List<DetalleAsistenciaDto> DetallesVoluntarios { get; set; } = [];
}
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
