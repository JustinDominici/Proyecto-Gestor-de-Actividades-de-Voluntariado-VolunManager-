namespace VolunManager.Application.Dtos.Tareas
{
    public class TareaDto
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public string Estado { get; set; } = string.Empty;

        public int JornadaId { get; set; }

        public string? TituloJornada { get; set; }

        public int VoluntarioId { get; set; }

        public string? NombreVoluntario { get; set; }

        public DateTime FechaAsignacion { get; set; }

        public DateTime? FechaCompletada { get; set; }
    }
}
