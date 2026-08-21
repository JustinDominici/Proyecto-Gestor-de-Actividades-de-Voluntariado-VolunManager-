namespace VolunManager.Application.Dtos.Asistencias
{
    public class AsistenciaUpdateDto
    {
        // Solo se editan los horarios: el voluntario y la jornada de un
        // registro de asistencia no cambian una vez creado.
        public DateTime HoraEntrada { get; set; }

        public DateTime? HoraSalida { get; set; }
    }
}
