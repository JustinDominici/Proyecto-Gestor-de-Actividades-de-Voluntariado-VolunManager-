using VolunManager.Domain.Enums;

namespace VolunManager.Application.Dtos.Tareas
{
    public class TareaUpdateDto
    {
        public string Titulo { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public int JornadaId { get; set; }

        public int VoluntarioId { get; set; }

        public EstadoTarea Estado { get; set; }
    }
}
