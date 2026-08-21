namespace VolunManager.Application.Dtos.Asistencias
{
    public class AsistenciaCreateDto
    {
        public int VoluntarioId { get; set; }

        public int JornadaId { get; set; }

        public DateTime HoraEntrada { get; set; }

        public DateTime? HoraSalida { get; set; }
    }
}
