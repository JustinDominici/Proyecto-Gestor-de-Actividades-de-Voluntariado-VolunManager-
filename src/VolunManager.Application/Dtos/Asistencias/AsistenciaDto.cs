namespace VolunManager.Application.Dtos.Asistencias
{
    public class AsistenciaDto
    {
        public int Id { get; set; }

        public int VoluntarioId { get; set; }

        public string? NombreVoluntario { get; set; }

        public int JornadaId { get; set; }

        public string? TituloJornada { get; set; }

        public DateTime HoraEntrada { get; set; }

        public DateTime? HoraSalida { get; set; }

        public double HorasTrabajadas { get; set; }
    }
}
