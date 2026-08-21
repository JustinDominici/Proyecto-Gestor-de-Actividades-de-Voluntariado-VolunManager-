namespace VolunManager.Application.Dtos.Tareas
{
    public class TareaCreateDto
    {
        public string Titulo { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public int JornadaId { get; set; }

        public int VoluntarioId { get; set; }
    }
}
